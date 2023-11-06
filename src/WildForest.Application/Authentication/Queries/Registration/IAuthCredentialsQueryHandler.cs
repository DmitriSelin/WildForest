namespace WildForest.Application.Authentication.Queries.Registration;

public interface IAuthCredentialsQueryHandler
{
    Task<AuthCredentials> GetCountriesAndLanguagesAsync();
}