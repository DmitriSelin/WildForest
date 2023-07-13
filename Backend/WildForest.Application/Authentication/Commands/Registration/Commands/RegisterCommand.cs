namespace WildForest.Application.Authentication.Commands.Registration.Commands;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string IpAddress,
    Guid CityId);