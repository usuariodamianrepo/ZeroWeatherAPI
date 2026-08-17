using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Web.Dtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            var cities = await _cityService.GetAll();

            return Ok(cities.Adapt<IEnumerable<CityDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            if (id == 0)
                return BadRequest("The City Id can not be 0.");

            var city = await _cityService.GetCityById(id);

            return Ok(city.Adapt<CityDto>());
        }

        [HttpPost]
        public async Task<ActionResult<CityDto>> Post([FromBody] CitySaveDto city)
        {
            try
            {
                var createdCity = await _cityService.CreateCity(city.Adapt<City>());

                return Ok(createdCity.Adapt<CityDto>());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CitySaveDto city)
        {
            if (id == 0)
                return BadRequest("The City Id can not be 0.");

            try
            {
                var updatedCity = await _cityService.UpdateCity(id, city.Adapt<City>());
                return Ok(updatedCity.Adapt<CityDto>());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CitySimpleDto>> Delete(int id)
        {
            if (id == 0)
                return BadRequest("The City Id can not be 0.");

            try
            {
                await _cityService.DeleteCity(id);
                return Ok(new CitySimpleDto());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }
        }
    }
}
