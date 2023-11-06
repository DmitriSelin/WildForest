namespace WildForest.Application.Authentication.Queries.Registration;

public sealed record AuthCredentials(
    IEnumerable<UserCredentials> Countries,
    IEnumerable<UserCredentials> Languages);