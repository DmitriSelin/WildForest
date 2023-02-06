using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Http.Weather;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IWeatherForecastHttpClient _httpClient;

        public TestController(IWeatherForecastHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> TestGetWeather()
        {
            Guid cityId = Guid.Parse("a0b3096c-f43f-40bc-9c26-186c9462a42b");

            await _httpClient.GetWeatherForecastAsync(cityId);

            return Ok();
        }
    }
}