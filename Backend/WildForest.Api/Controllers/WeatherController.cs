using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Authentication;

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

        public async Task<IActionResult> GetTodayWeather()
        {
            var userId = _jwtTokenDecoder.GetUserIdFromToken(HttpContext.Request);

                        

            return Ok();
        }
    }
}