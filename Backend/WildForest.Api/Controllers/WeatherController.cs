using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    [Authorize]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetTodayWeather([FromQuery] Guid cityId)
        {
            // Find city in db

            // Get weather:
            // Temperature, description,
            // humidity, pressure,
            // wind, type of weather,
            // cloudness, storm

            return Ok();
        }
    }
}