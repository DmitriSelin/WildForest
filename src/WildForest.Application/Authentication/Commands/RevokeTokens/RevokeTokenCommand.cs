namespace WildForest.Application.Authentication.Commands.RevokeTokens;

public sealed record RevokeTokenCommand(string Token, string IpAddress);