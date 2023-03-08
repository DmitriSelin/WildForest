namespace WildForest.Application.Authentication.Queries.LoginUser
{
    public sealed record LoginUserQuery(string Email, string Password, string IpAddress);
}