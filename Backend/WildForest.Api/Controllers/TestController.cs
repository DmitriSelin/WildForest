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
            Guid cityId = Guid.Parse("c964e75d-d3d7-4da8-bc77-2faa55e3e512");

            await _httpClient.GetWeatherForecastAsync(cityId);

            return Ok();
        }
    }
}