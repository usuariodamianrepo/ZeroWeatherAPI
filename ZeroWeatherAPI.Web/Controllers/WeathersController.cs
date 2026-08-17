using Mapster;
using Microsoft.AspNetCore.Mvc;
using ZeroWeatherAPI.Core.Entities;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Web.Dtos;

namespace ZeroWeatherAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeathersController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherDto>>> Get()
        {
            var weathers = await _weatherService.GetAll();

            return Ok(weathers.Adapt<IEnumerable<WeatherDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherDto>> Get(int id)
        {
            var weather = await _weatherService.GetWeatherById(id);

            return Ok(weather.Adapt<WeatherDto>());
        }

        [HttpPost]
        public async Task<ActionResult<WeatherDto>> Post([FromBody] WeatherDto Weather)
        {
            try
            {
                var createdWeather = await _weatherService.CreateWeather(Weather.Adapt<Weather>());

                return Ok(createdWeather.Adapt<WeatherDto>());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WeatherDto>> Put(int id, [FromBody] WeatherDto Weather)
        {
            try
            {
                var updatedWeather = await _weatherService.UpdateWeather(id, Weather.Adapt<Weather>());

                return Ok(updatedWeather.Adapt<WeatherDto>());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            await _weatherService.DeleteWeather(id);
            return Ok($"The Weather Id:{id} was deleted.");
        }

        [HttpGet("get-and-save")]
        public async Task<ActionResult<IEnumerable<WeatherDetailDto>>> GetWeatherAndSaveInfo(int id, bool showHistorical = false, int take = 10)
        {
            if (id == 0)
                return BadRequest("The City Id can not be 0. Select one City.");

            try
            {
                List<Weather> weathers = new();

                var weather = await _weatherService.GetWeatherAndSaveInfo(id, take);
                weathers.Add(weather);

                if (showHistorical)
                {
                    weathers.Clear();
                    var historical = await _weatherService.GetLastAsync(id, take);
                    weathers.AddRange(historical);
                }

                return Ok(weathers.Adapt<IEnumerable<WeatherDetailDto>>());
            }
            catch (Exception ex)
            {
                return Conflict("Error trying to save.");
            }

        }
    }
}
