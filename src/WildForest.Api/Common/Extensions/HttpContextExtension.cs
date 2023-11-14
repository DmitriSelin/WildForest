using ErrorOr;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace WildForest.Api.Common.Extensions;

public static class HttpContextExtension
{
    public static string GetIpAddress(this HttpContext context)
    {
        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            return context.Request.Headers["X-Forwarded-For"]!;
        else
            return context.Connection.RemoteIpAddress!.MapToIPv4().ToString();
    }

    public static ErrorOr<Guid> GetUserIdFromAuthHeader(this HttpContext context)
    {
        context.Request.Headers.TryGetValue("Authorization", out StringValues bearer);

        var userId = Guid.Empty;

        if (bearer.Any())
        {
            string? token = bearer[0]?.Split(" ")[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = tokenHandler.ReadJwtToken(token);

            userId = Guid.Parse(jwt.Claims.First(c => c.Type.Equals("sub")).Value);
        }

        if (userId == Guid.Empty)
            return Domain.Common.Errors.Errors.Authentication.InvalidAuthorizationHeader;

        return userId;
    }
}