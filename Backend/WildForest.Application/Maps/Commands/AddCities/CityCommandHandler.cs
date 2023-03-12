using System.Text.Json;
using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Maps.Commands.AddCities;

public sealed class CityCommandHandler : ICityCommandHandler
{
    private readonly ICountryRepository _countryRepository;
    private readonly ICityRepository _cityRepository;

    public CityCommandHandler(ICountryRepository countryRepository, ICityRepository cityRepository)
    {
        _countryRepository = countryRepository;
        _cityRepository = cityRepository;
    }

    public async Task<ErrorOr<string>> AddCitiesFromJsonFileAsync(CityCommand command)
    {
        var countryId = CountryId.Create(command.CountryId);
        var country = await _countryRepository.GetCountryByIdAsync(countryId);

        if (country is null)
        {
            return Errors.Country.NotFound;
        }

        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new CityConverter(country.Id));

        var path = "D:\\Projects\\WildForest\\Backend\\WildForest.Application\\Maps\\Commands\\AddCities\\Data";

        await using var fs = new FileStream($"{path}/{command.FileName}.json", FileMode.Open);
        var cities = await JsonSerializer.DeserializeAsync(fs, typeof(List<City>), jsonOptions) as List<City>;

        if (cities is null)
        {
            throw new ArgumentNullException(nameof(cities));
        }

        await _cityRepository.AddCitiesAsync(cities);

        var result = "The cities were successfully added";
        return result;
    }
}