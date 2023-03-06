using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Common.Interfaces.Services;

namespace WildForest.Api.Controllers;

[AllowAnonymous]
[Microsoft.AspNetCore.Components.Route("token")]
public sealed class TokenController : ApiController
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public TokenController(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        
        return Ok();
    }

    private void SetTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = _dateTimeProvider.UtcNow.AddDays(7)
        };
        
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    private string GetIPAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
            return Request.Headers["X-Forwarded-For"]!;
        else
            return HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
    }
}