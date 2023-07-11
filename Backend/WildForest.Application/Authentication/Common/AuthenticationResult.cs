namespace WildForest.Application.Authentication.Common;

public sealed class AuthenticationResult<T>
    where T : class
{
    public T User { get; }

    public string AccessToken { get; }

    public string RefreshToken { get; }

    public AuthenticationResult(T user, string accessToken, string refreshToken)
    {
        User = user;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
}