using Arabytak.Core.Entities;
using Arabytak.Core.Repositories.Contract;
using Arabytak.Core.Specification.CarSpecification;
using ARABYTAK.APIS.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ARABYTAK.APIS.Controllers
{
    [Authorize]
    public class CarController : BaseApiController
    {
               private readonly IMapper _mapper;
               private readonly IUnitOfWork _uniteOfWork;
        
        public CarController(IMapper mapper, IUnitOfWork uniteOfWork )
        {  
            _mapper = mapper;
            _uniteOfWork = uniteOfWork;
           
        }
        [HttpGet("{status}/{CarId}")]
        public async Task<ActionResult<CarDto>> GetCarDetails(int CarId,[FromQuery] Status status)
        {
            var spec = new CarWithBrandAndCategoryAndPicUrlAndSpecSpecification(CarId, status);
            var car = await _uniteOfWork.Repositorey<Car>().GetByIdWithSpecAsync(spec);
            if(car==null)
            {
                return NotFound();
            }
           var carDto=_mapper.Map<Car,CarDto>(car);
            carDto.CarName = $"{car.brand.Name} {car.model.Name}";
            if(status==Status.New)
            {
                var specDto = _mapper.Map<SpecNewCar, SpecNewCarDto>(car.specNewCar);
                carDto.Specifications= specDto;
            }
            else if(status==Status.Used)
            {
                var specDto= _mapper.Map<SpecUsedCar,SpecUsedCarDto>(car.specUsedCar);
                carDto.Specifications= specDto;
            }
           // carDto.DealershipName = car.dealership.Name;
           
            return Ok(carDto);
        }



        [HttpGet("Brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrand()
        {
            var brands= await _uniteOfWork.Repositorey<Brand>().GetAllAsync();
            return Ok(brands);
        }


        [HttpGet("Dealership")]
        public async Task<ActionResult<IReadOnlyList<Dealership>>> GetDealerShip()
        {
            var dealerships= await _uniteOfWork.Repositorey<Dealership>().GetAllAsync();
            return Ok(dealerships);
        }


        [HttpGet("RescueCompany")]
        public async Task<ActionResult<IReadOnlyList<RescueCompany>>> GetRescueCompany()
        {
            var RescueCompany=  await _uniteOfWork.Repositorey<RescueCompany>().GetAllAsync();
            return Ok(RescueCompany);
        }
    }
}
