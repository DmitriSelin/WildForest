namespace WildForest.Application.Authentication.Commands.RegisterUser
{
    public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string IpAddress,
        Guid CityId);
}