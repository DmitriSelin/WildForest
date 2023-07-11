namespace WildForest.Application.Authentication.Commands.Registration.Commands;

public class RegisterCommand
{
    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public string Password { get; }

    public string IpAddress { get; }

    public RegisterCommand(
        string firstname, string lastName,
        string email, string password,
        string ipAddress)
    {
        FirstName = firstname;
        LastName = lastName;
        Email = email;
        Password = password;
        IpAddress = ipAddress;
    }
}