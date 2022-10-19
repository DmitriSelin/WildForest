using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Contracts.Authentication.Requests;
using WildForest.Contracts.Authentication.Responses;

namespace WildForest.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRegistrator _userRegistrator;

        public AuthenticationController(IUserRegistrator userRegistrator)
        {
            _userRegistrator = userRegistrator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var authenticationResult = _userRegistrator.Register(command);

            var response = new AuthenticationResponse(
                authenticationResult.User.Id,
                authenticationResult.User.FirstName,
                authenticationResult.User.LastName,
                authenticationResult.User.Email,
                authenticationResult.User.Password,
                authenticationResult.Token);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return NotFound();
        }
    }
}