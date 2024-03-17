namespace WildForest.Application.Authentication.Commands.RefreshTokens;

public sealed record RefreshTokenCommand(string Token, string IpAddress);