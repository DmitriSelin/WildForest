using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Application.Weather.Queries.DayWeather;
using WildForest.Domain.City.ValueObjects;
using WildForest.Domain.User.ValueObjects;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    public sealed class WeatherForecastController : ApiController
    {
        private readonly IJwtTokenDecoder _jwtTokenDecoder;

        public WeatherForecastController(IJwtTokenDecoder jwtTokenDecoder)
        {
            _jwtTokenDecoder = jwtTokenDecoder;
        }

        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetTodayWeather([FromQuery] Guid cityId)
        {
            var userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

            var query = new DayWeatherQuery(
                UserId.CreateUserId(userId),
                CityId.CreateCityId(cityId));

            

            return Ok();
        }
    }
}