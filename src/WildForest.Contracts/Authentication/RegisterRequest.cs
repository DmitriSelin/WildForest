namespace WildForest.Contracts.Authentication;

public class RegisterRequest
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string Password { get; init; }

    public Guid CityId { get; init; }

    public Guid LanguageId { get; init; }

    public RegisterRequest(
        string firstName, string lastName, string email,
        string password, Guid cityId, Guid languageId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CityId = cityId;
        LanguageId = languageId;
    }
}