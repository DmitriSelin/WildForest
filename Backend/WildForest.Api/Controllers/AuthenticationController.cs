using Microsoft.AspNetCore.Mvc;
using WildForest.Contracts.Authentication.Requests;

namespace WildForest.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {

        }
    }
}