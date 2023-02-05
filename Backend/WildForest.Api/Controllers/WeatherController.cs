using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Http.Jwt;
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
        public async Task<IActionResult> GetWeather(Guid cityId, DateOnly forecastDate)
        {
            ErrorOr<Guid> userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

            if (userId.IsError)
            {
                return Problem(userId.Errors);
            }

            var query = new ForecastQuery(userId.Value, cityId, forecastDate);

            ErrorOr<List<WeatherForecastDto>> forecust = await _weatherForecastDetector.GetWeatherForecast(query);

            return Ok(forecust.Value);
        }
    }
}