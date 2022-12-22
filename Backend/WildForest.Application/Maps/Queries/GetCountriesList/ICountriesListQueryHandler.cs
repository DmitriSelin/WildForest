using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Maps.Queries.GetCountriesList
{
    public interface ICountriesListQueryHandler
    {
        Task<List<Country>> GetCountriesAsync();
    }
}