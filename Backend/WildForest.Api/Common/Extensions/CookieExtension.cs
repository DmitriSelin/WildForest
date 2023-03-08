namespace WildForest.Api.Common.Extensions;

public static class CookieExtension
{
    public static void SetTokenCookie(this IResponseCookies cookies, string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        
        cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}