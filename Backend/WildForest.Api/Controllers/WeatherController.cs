using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Weather.Common;
using WildForest.Application.Weather.Queries.GetTodayForecast;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

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

        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetTodayWeather(Guid cityId)
        {
            UserId userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

            var query = new TodayForecastQuery(userId, CityId.CreateCityId(cityId));

            ErrorOr<WeatherForecust> forecust = await _weatherForecastDetector.GetTodayWeatherForecast(query);

            return Ok(forecust);
        }
    }
}