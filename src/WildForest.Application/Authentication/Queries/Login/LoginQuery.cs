namespace WildForest.Application.Authentication.Queries.LoginUser;

public sealed record LoginQuery(string Email, string Password, string IpAddress);