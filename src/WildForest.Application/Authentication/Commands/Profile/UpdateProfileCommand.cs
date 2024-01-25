using WildForest.Application.Common.Models;

namespace WildForest.Application.Authentication.Commands.Profile;

public sealed class UpdateProfileCommand : RegisterUserDto
{
    public string? NewPassword { get; init; }

    public string IpAddress { get; init; }

    public UpdateProfileCommand(
        Guid id, string firstName, string lastName,
        string email, string password, Guid cityId,
        Guid languageId, string? newPassword,
        string ipAddress) : base(id, firstName, lastName, email, password, cityId, languageId)
    {
        NewPassword= newPassword;
        IpAddress= ipAddress;
    }
}