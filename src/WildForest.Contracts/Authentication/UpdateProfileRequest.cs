namespace WildForest.Contracts.Authentication;

public sealed class UpdateProfileRequest : RegisterRequest
{
    public string? NewPassword { get; init; }

    public UpdateProfileRequest(
        string firstName, string lastName, string email, string password,
        string? newPassword, string? image, Guid cityId, Guid languageId) : base(firstName, lastName, email, password, image, cityId, languageId)
    {
        NewPassword = newPassword;
    }
}