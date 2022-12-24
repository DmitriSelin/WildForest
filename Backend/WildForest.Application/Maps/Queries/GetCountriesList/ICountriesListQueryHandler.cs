using WildForest.Application.Maps.Common;

namespace WildForest.Application.Maps.Queries.GetCountriesList
{
    public interface ICountriesListQueryHandler
    {
        Task<List<CountryViewModel>> GetCountriesAsync();
    }
}