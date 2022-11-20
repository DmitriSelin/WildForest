using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    [Authorize]
    public sealed class WeatherForecastController : ApiController
    {
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetTodayWeather([FromQuery] Guid cityId)
        {
            var query = new DayWeatherQuery();
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