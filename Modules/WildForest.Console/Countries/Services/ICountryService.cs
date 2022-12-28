using WildForest.Domain.Countries.Entities;

namespace WildForest.Console.Countries.Services
{
    public interface ICountryService
    {
        Task AddCountryAsync(Country country);
    }
}
