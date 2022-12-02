using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Application.Weather.Queries.GetTodayForecast;
using WildForest.Domain.Cities.ValueObjects;
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

            ErrorOr<CityId> cityId = await _cityService.GetCityIdAsync(userId);
            
            if (cityId.IsError)
            {
                return Problem(cityId.Errors);
            }

            var query = new TodayForecastQuery(userId, cityId.Value);

            var weatherForecast = _weatherForecastDetector.GetTodayWeatherForecast(query);

            return Ok(weatherForecast);
        }
    }
}