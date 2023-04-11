using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Api.Controllers
{
    [ApiController]
    [Route("test")]
    public class TestController : ControllerBase
    {
        private readonly WildForestDbContext _context;

        public TestController(WildForestDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> TestEf()
        {
            var date = ForecastDate.Create(DateOnly.FromDateTime(DateTime.Now.Date));
            var cityId = CityId.Create(Guid.Parse("e1846ac0-66fa-42db-a9da-e6f12e54bf47"));

            var forecasts = await _context.WeatherForecasts
                .Include(x => x.WeatherMark)
                .Where(x => x.CityId == cityId && x.ForecastDate.Value == date.Value)
                .ToListAsync();

            return Ok(forecasts);
        }
    }
}