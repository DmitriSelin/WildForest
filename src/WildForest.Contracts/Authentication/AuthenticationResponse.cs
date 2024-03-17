using WildForest.Dto.Models;

namespace WildForest.Contracts.Authentication;

public sealed class AuthenticationResponse : RegisterUserDto
{
    public string CityName { get; init; }

    public string Language { get; init; }

    public string Token { get; init; }

    public AuthenticationResponse(
        Guid id, string firstName,
        string lastName, string email,
        string password, Guid cityId,
        string cityName, Guid languageId,
        string language, string token,
        byte[]? image) : base(id, firstName, lastName, email, password, cityId, languageId, image)
    {
        CityName = cityName;
        Language = language;
        Token = token;
    }
}