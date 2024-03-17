using WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<IEnumerable<Country>> GetAllCountriesAsync();

    Task<Country?> GetCountryByNameAsync(CountryName countryName);

    Task<Country?> GetCountryByIdAsync(CountryId countryId);
}