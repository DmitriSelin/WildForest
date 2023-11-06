namespace WildForest.Contracts.Authentication
{
    public sealed record AuthenticationResponse(
        Guid Id, 
        string FirstName,
        string LastName,
        string Email,
        string Password,
        Guid CityId,
        string CityName,
        Guid LanguageId,
        string Token);
}