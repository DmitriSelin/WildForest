namespace WildForest.Application.Authentication.Commands.RefreshToken;

public sealed record RefreshTokenCommand(string Token, string IpAddress);