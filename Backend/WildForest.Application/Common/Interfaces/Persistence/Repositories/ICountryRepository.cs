using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();

        Task AddCountryAsync(Country country);

        Task<Country?> GetCountryByNameAsync(CountryName countryName);
    }
}