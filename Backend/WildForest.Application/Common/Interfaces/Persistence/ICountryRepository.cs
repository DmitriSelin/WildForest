using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface ICountryRepository
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
    }
}