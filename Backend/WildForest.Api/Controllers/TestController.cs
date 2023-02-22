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
            var cities = await _context.Users
                .Include(p => p.City)
                .ToListAsync();

            string[] array = new string[2];
            int count = 0;

            foreach(var item in cities)
            {
                array[count] = item.City.CityName.Value;
                count++;
            }

            return Ok(array);
        }
    }
}