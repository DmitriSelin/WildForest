using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface ICityRepository
    {
        Task<City?> GetCityByIdAsync(CityId cityId);

        Task<IEnumerable<City>?> GetCitiesByCountryIdAsync(CountryId countryId);
    }
}