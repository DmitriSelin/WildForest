namespace WildForest.Application.Authentication.Commands.Registration;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    byte[]? Image,
    string IpAddress,
    Guid CityId,
    Guid LanguageId);