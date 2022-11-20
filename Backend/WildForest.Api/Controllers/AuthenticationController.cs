using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Contracts.Authentication.Requests;
using WildForest.Contracts.Authentication.Responses;
using WildForest.Domain.Common.Exceptions;

namespace WildForest.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public sealed class AuthenticationController : ApiController
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

            ErrorOr<AuthenticationResult> authenticationResult = await _userRegistrator.RegisterAsync(command);

            return authenticationResult.Match(
                authenticationResult => Ok(MapAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = new LoginUserQuery(request.Email, request.Password);

            var authenticationResult = await _userLogger.LoginAsync(query);

            if (authenticationResult.IsError && 
                (authenticationResult.FirstError == Errors.Authentication.InvalidEmail || authenticationResult.FirstError == Errors.Authentication.InvalidPassword))
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authenticationResult.FirstError.Description);
            }

            return authenticationResult.Match(
                authenticationResult => Ok(MapAuthenticationResult(authenticationResult)),
                errors => Problem(errors));
        }

        private static AuthenticationResponse MapAuthenticationResult(AuthenticationResult result)
        {
            return new AuthenticationResponse(
                result.User.Id.Value,
                result.User.FirstName,
                result.User.LastName,
                result.User.Email,
                result.User.Password,
                result.Token);
        }
    }
}