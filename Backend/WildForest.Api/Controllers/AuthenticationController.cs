using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Contracts.Authentication.Requests;
using WildForest.Contracts.Authentication.Responses;

namespace WildForest.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public sealed class AuthenticationController : ControllerBase
    {
        private readonly IUserRegistrator _userRegistrator;
        private readonly IUserLogger _userLogger;

        public AuthenticationController(IUserRegistrator userRegistrator, IUserLogger userLogger)
        {
            _userRegistrator = userRegistrator;
            _userLogger = userLogger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var authenticationResult = await _userRegistrator.RegisterAsync(command);

            var response = new AuthenticationResponse(
                authenticationResult.User.Id.Value,
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
            var query = new LoginUserQuery(request.Email, request.Password);

            var authenticationResult = await _userLogger.LoginAsync(query);

            var response = new AuthenticationResponse(
                authenticationResult.User.Id.Value,
                authenticationResult.User.FirstName,
                authenticationResult.User.LastName,
                authenticationResult.User.Email,
                authenticationResult.User.Password,
                authenticationResult.Token);

            return Ok(response);
        }
    }
}