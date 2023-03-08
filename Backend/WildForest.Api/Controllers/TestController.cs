using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WildForest.Api.Services.Weather;
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
        public async Task<IActionResult> Get()
        {
            var user = await _context.RefreshTokens.Include(x => x.User)
                .SingleOrDefaultAsync(j => j.Token.Value == "");
            
            if (user != null)
                return Ok(user);

            return Ok("User is null");
        }
    }
}