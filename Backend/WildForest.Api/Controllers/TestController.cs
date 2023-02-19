using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Http.Weather;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IWeatherForecastHttpClient _httpClient;
        private readonly WildForestDbContext _context;

        public TestController(IWeatherForecastHttpClient httpClient, WildForestDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> TestGetWeather()
        {
            Guid cityId = Guid.Parse("a0b3096c-f43f-40bc-9c26-186c9462a42b");

            var weather = await _httpClient.GetWeatherForecastAsync(CityId.Create(cityId));

            return Ok(weather);
        }
    }
}