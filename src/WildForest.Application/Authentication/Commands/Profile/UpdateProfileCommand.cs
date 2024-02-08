using WildForest.Dto.Models;

namespace WildForest.Application.Authentication.Commands.Profile;

public sealed class UpdateProfileCommand : RegisterUserDto
{
    public string? NewPassword { get; init; }

    public string IpAddress { get; init; }

    public UpdateProfileCommand(
        Guid id, string firstName, string lastName,
        string email, string password, Guid cityId,
        Guid languageId, string? newPassword,
        string ipAddress, byte[]? image) : base(id, firstName, lastName, email, password, cityId, languageId, image)
    {
        NewPassword = newPassword;
        IpAddress = ipAddress;
        Image = image;
    }
}