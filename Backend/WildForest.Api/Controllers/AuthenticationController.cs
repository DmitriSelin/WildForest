using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Contracts.Authentication;
using WildForest.Domain.Common.Exceptions;

namespace WildForest.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public sealed class AuthenticationController : ApiController
    {
        private readonly IUserRegistrator _userRegistrator;
        private readonly IUserLogger _userLogger;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IUserRegistrator userRegistrator,
            IUserLogger userLogger,
            IMapper mapper)
        {
            _userRegistrator = userRegistrator;
            _userLogger = userLogger;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterUserCommand>(request);

            ErrorOr<AuthenticationResult> authenticationResult = await _userRegistrator.RegisterAsync(command);

            return authenticationResult.Match(
                authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
                errors => Problem(errors));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginUserQuery>(request);

            var authenticationResult = await _userLogger.LoginAsync(query);

            if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authenticationResult.FirstError.Description);
            }

            return authenticationResult.Match(
                authenticationResult => Ok(_mapper.Map<AuthenticationResponse>(authenticationResult)),
                errors => Problem(errors));
        }
    }
}