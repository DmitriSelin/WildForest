namespace WildForest.Application.Authentication.Commands.Profile;

public sealed record UpdateProfileCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string IpAddress,
    Guid CityId,
    Guid LanguageId);