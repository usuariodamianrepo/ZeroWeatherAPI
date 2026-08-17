using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces.Shared;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenWeathersController : ControllerBase
    {
        private readonly IOpenWeatherService _openWeatherService;

        public OpenWeathersController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet("by-lat-lon")]
        public async Task<Root> Get(decimal latitude, decimal longitude)
        {
            Root result = await _openWeatherService.GetWeatherAsync(latitude, longitude);
            return result;
        }

        [HttpGet("by-city-country")]
        public async Task<Root> Get(string city, string country)
        {
            Root result = await _openWeatherService.GetWeatherAsync(city, country);
            return result;
        }
    }
}
