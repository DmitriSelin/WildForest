using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Authentication.Commands.RefreshTokens;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Controllers;

[AllowAnonymous]
[Microsoft.AspNetCore.Components.Route("token")]
public sealed class TokenController : ApiController
{
    private readonly IRefreshTokenCommandHandler _refreshTokenCommandHandler;
    private readonly IMapper _mapper;
    
    public TokenController(IRefreshTokenCommandHandler refreshTokenCommandHandler, IMapper mapper)
    {
        _refreshTokenCommandHandler = refreshTokenCommandHandler;
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
}