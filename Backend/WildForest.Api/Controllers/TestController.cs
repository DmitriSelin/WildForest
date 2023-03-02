using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Services.Weather;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        public TestController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _service.AddWeatherForecastsToDb();
            
            return Ok();
        }
    }
}