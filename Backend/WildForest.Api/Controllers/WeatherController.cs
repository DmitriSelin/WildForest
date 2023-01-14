using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Http;
using WildForest.Application.Weather.Common;
using WildForest.Application.Weather.Queries.GetWeatherForecast;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    public sealed class WeatherForecastController : ApiController
    {
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        private readonly IWeatherForecastDetector _weatherForecastDetector;

        public WeatherForecastController(
            IJwtTokenDecoder jwtTokenDecoder,
            IWeatherForecastDetector weatherForecastDetector)
        {
            _jwtTokenDecoder = jwtTokenDecoder;
            _weatherForecastDetector = weatherForecastDetector;
        }

        [HttpGet("{cityId}/{weatherDate}")]
        public async Task<IActionResult> GetWeather(Guid cityId, DateTime weatherDate)
        {
            ErrorOr<Guid> id = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

            if (id.IsError)
            {
                return Problem(id.Errors);
            }

            Guid userId = id.Value;

            var query = new ForecastQuery(userId, cityId, weatherDate);

            ErrorOr<List<WeatherForecust>> forecust = await _weatherForecastDetector.GetWeatherForecast(query);

            return Ok(forecust.Value);
        }
    }
}