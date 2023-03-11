using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Application.Authentication.Commands.RevokeTokens;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Controllers;

[AllowAnonymous]
[Route("api/tokens")]
public sealed class TokenController : ApiController
{
    private readonly IRefreshTokenCommandHandler _refreshTokenCommandHandler;
    private readonly IRevokeTokenCommandHandler _revokeTokenCommandHandler;
    private readonly IMapper _mapper;
    
    public TokenController(
        IRefreshTokenCommandHandler refreshTokenCommandHandler,
        IRevokeTokenCommandHandler revokeTokenCommandHandler,
        IMapper mapper)
    {
        _refreshTokenCommandHandler = refreshTokenCommandHandler;
        _revokeTokenCommandHandler = revokeTokenCommandHandler;
        _mapper = mapper;
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var iPAddress = HttpContext.GetIpAddress();

        var command = new RefreshTokenCommand(refreshToken!, iPAddress);
        
        var authenticationResult = await _refreshTokenCommandHandler.RefreshTokenAsync(command);

        if (authenticationResult.IsError)
        {
            return Problem(authenticationResult.Errors);
        }
        
        HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var authenticationResponse = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);
        return Ok(authenticationResponse);
    }

    [Authorize]
    [HttpPost("revokeToken")]
    public async Task<IActionResult> RevokeToken(RevokeTokenRequest? request)
    {
        var token = request?.Token ?? Request.Cookies["refreshToken"];
        var iPAddress = HttpContext.GetIpAddress();

        if (string.IsNullOrEmpty(token))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Token is required");
        }

        var command = new RevokeTokenCommand(token, iPAddress);
        
        ErrorOr<string> result = await _revokeTokenCommandHandler.RevokeRefreshTokenAsync(command);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result.Value);
    }
}