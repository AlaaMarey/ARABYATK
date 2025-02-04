using Arabytak.Core.Entities.Identity_Entities;
using Arabytak.Core.Repositories.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Arabytak.Repository.Repository.Contract
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public string CreateJWTAsync(ApplicationUser AppUser)
        {

            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , AppUser.Email),
                new Claim(ClaimTypes.GivenName , AppUser.UserName)
            };


            var Credientials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);


            var JWTDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(Claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(2),
                //NotBefore = DateTime.UtcNow,
                Issuer = _configuration["Token:Issuer"],
                Audience = _configuration["Token:Audience"],
                SigningCredentials = Credientials
            };


            var handleJWTToken = new JwtSecurityTokenHandler();
            var JWTToken = handleJWTToken.CreateToken(JWTDiscriptor);

            return handleJWTToken.WriteToken(JWTToken);


        }



    }
}
