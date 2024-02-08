namespace WildForest.Contracts.Authentication;

public class RegisterRequest
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string Password { get; init; }

    public string? Image { get; init; }

    public Guid CityId { get; init; }

    public Guid LanguageId { get; init; }

    public RegisterRequest(
        string firstName, string lastName, string email,
        string password, string? image, Guid cityId, Guid languageId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Image = image;
        CityId = cityId;
        LanguageId = languageId;
    }
}