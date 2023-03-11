using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Maps.Commands.AddCountry;

public sealed class CountryCommandHandler : ICountryCommandHandler
{
    private readonly ICountryRepository _countryRepository;

    public CountryCommandHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<ErrorOr<string>> AddCountryAsync(CountryCommand command)
    {
        var countryName = CountryName.Create(command.CountryName);

        var country = await _countryRepository.GetCountryByNameAsync(countryName);

        if (country is not null)
        {
            return Errors.Country.Duplicate;
        }

        country = Country.Create(countryName);

        await _countryRepository.AddCountryAsync(country);

        var result = "The country was successfully added";
        return result;
    }
}