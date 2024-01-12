namespace WildForest.Contracts.Authentication;

public sealed class UpdateProfileRequest : RegisterRequest
{
    public string? NewPassword { get; init; }

    public UpdateProfileRequest(
        string firstName, string lastName, string email, string password,
        string? newPassword, Guid cityId, Guid languageId) : base(firstName, lastName, email, password, cityId, languageId)
    {
        NewPassword = newPassword;
    }
}