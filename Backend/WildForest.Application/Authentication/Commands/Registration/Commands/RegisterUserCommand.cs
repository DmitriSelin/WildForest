namespace WildForest.Application.Authentication.Commands.Registration.Commands;

public sealed class RegisterUserCommand : RegisterCommand
{
    public Guid CityId { get; }

    public RegisterUserCommand(
        string firstName, string lastName,
        string email, string password,
        string ipAddress, Guid cityId) : base(firstName, lastName,
                                                email, password, ipAddress)
    {
        CityId = cityId;
    }
}