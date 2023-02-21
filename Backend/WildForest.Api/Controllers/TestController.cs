using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("ef")]
        public async Task<IActionResult> Get()
        {
            var cities = await _context.Cities
                .Include(x => x.Users)
                .Where(x => x.CityName.Value == "Krasnodar")
                .ToListAsync();

            return Ok(cities);
        }
    }
}