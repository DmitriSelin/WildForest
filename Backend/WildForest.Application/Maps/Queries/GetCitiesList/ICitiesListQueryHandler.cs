using ErrorOr;

namespace WildForest.Application.Maps.Queries.GetCitiesList
{
    public interface ICitiesListQueryHandler
    {
        Task<ErrorOr<List<CityQuery>>> GetCitiesByCountryIdAsync(Guid countryId);
    }
}