using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Http.Jwt;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Api.Controllers
{
    [Route("api/weather/forecast")]
    public sealed class WeatherForecastController : ApiController
    {
        private readonly IJwtTokenDecoder _jwtTokenDecoder;
        private readonly IHomeWeatherForecastHandler _homeWeatherForecastHandler;  

        public WeatherForecastController(
            IJwtTokenDecoder jwtTokenDecoder,
            IHomeWeatherForecastHandler homeWeatherForecastHandler)
        {
            _jwtTokenDecoder = jwtTokenDecoder;
            _homeWeatherForecastHandler = homeWeatherForecastHandler;
        }

        [HttpGet("homeCity/{forecastDate}")]
        public async Task<IActionResult> GetWeather([FromRoute] DateOnly forecastDate)
        {
            ErrorOr<Guid> userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

            if (userId.IsError)
            {
                return Problem(userId.Errors);
            }
            
            var query = new HomeWeatherForecastQuery(userId.Value, forecastDate);
            
            var forecasts = await _homeWeatherForecastHandler.GetWeatherForecastAsync(query);
            
            return Ok(forecasts);
        }
    }
}