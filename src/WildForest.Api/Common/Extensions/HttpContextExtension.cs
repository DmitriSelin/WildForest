using Microsoft.Extensions.Primitives;

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

    public static StringValues GetJwtTokenFromAuthHeader(this HttpContext context)
    {
        context.Request.Headers.TryGetValue("Authorization", out StringValues token);

        return token;
    }
}