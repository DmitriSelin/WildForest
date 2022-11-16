namespace WildForest.Contracts.Authentication.Requests
{
    public sealed record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password);
}