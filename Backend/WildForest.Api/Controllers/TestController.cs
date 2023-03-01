using Microsoft.AspNetCore.Mvc;
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
            return Ok();
        }
    }
}