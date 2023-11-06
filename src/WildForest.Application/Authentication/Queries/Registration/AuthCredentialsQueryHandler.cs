using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;

namespace WildForest.Application.Authentication.Queries.Registration;

public sealed class AuthCredentialsQueryHandler : IAuthCredentialsQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public AuthCredentialsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthCredentials> GetCountriesAndLanguagesAsync()
    {
        var countries = await _unitOfWork.CountryRepository.GetAllCountriesAsync();
        var languages = await _unitOfWork.LanguageRepository.GetAllLanguagesAsync();

        var countiesCredentials = countries.Select(x => new UserCredentials(x.Id.Value, x.Name.Value));
        var languagesCredentials = languages.Select(x => new UserCredentials(x.Id.Value, x.Name));

        var authCredentials = new AuthCredentials(countiesCredentials, languagesCredentials);
        return authCredentials;
    }
}