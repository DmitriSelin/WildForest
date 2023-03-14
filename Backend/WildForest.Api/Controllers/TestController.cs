using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildForest.Api.Services.Http.Weather;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly WeatherForecastHttpClient _context;

        public TestController(WeatherForecastHttpClient context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cityId = CityId.Create();

            _context.GetWeatherForecastAsync(cityId);

            return Ok();
        }
    }
}