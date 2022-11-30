using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Application.Weather.Queries.GetTodayForecast;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    public sealed class WeatherForecastController : ApiController
    {
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        private readonly ICityService _cityService;
        private readonly IWeatherForecastDetector _weatherForecastDetector;

        public WeatherForecastController(
            IJwtTokenDecoder jwtTokenDecoder,
            ICityService cityService,
            IWeatherForecastDetector weatherForecastDetector)
        {
            _jwtTokenDecoder = jwtTokenDecoder;
            _cityService = cityService;
            _weatherForecastDetector = weatherForecastDetector;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodayWeather()
        {
            UserId userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);
            var cityId = await _cityService.GetCityIdAsync(userId);

            var query = new TodayForecastQuery(userId, cityId);

            var weatherForecast = _weatherForecastDetector.GetTodayWeatherForecast(query);

            return Ok(weatherForecast);
        }
    }
}