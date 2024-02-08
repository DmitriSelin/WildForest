namespace WildForest.Dto.Models;

public abstract class RegisterUserDto : UserDto
{
    public Guid CityId { get; init; }

    public Guid LanguageId { get; init; }

    public byte[]? Image { get; init; }

    protected RegisterUserDto(
        Guid id, string firstName,
        string lastName, string email,
        string password, Guid cityId,
        Guid languageId, byte[]? image) : base(id, firstName, lastName, email, password)
    {
        CityId = cityId;
        LanguageId = languageId;
        Image = image;
    }
}