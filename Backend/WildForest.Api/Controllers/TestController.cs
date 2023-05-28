using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IWeatherForecastRepository _weatherForecastRepository;

        public TestController(IWeatherForecastRepository weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TestEf()
        {
            Guid cityId = Guid.Parse("9fec3e39-a47f-4581-8d9b-416bdff66ec2");
            var forecasts = await _weatherForecastRepository.GetWeatherForecastsByCityIdAsync(CityId.Create(cityId));

            return Ok("Ok");
        }
    }
}