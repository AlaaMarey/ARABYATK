using Arabytak.Core.Entities.Identity_Entities;
using Arabytak.Core.Repositories.Contract;
using ARABYTAK.APIS.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ARABYTAK.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {




        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenServices _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;



        public AccountController(IMapper mapper, UserManager<ApplicationUser> userManager, ITokenServices tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }



        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUserDto RegisterDto)
        {
            var mappedUser = _mapper.Map<ApplicationUser>(RegisterDto);

            var result = await _userManager.CreateAsync(mappedUser, RegisterDto.Password);
            if (result.Succeeded)
            {
                return new UserDto
                {
                    Name = mappedUser.UserName,
                    Email = mappedUser.Email,
                };
            }

            return BadRequest();
        }



        [HttpPost("SignIn")]
        public async Task<ActionResult<UserDto>> SignIn(SignInUserDto SignInDto)
        {

            var User = await _userManager.FindByEmailAsync(SignInDto.Emial);

            if (User is not null)
            {

                //var result =  await  _userManager.CheckPasswordAsync(User, SignInDto.Password);
                var result = await _signInManager.CheckPasswordSignInAsync(User, SignInDto.Password, false);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Name = User.UserName,
                        Email = User.Email,
                        Token = _tokenService.CreateJWTAsync(User)
                    };
                }

            }

            return Unauthorized();


        }







    }

}
