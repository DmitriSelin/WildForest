using ErrorOr;
using WildForest.Application.Maps.Queries.GetCitiesList.Dto;

namespace WildForest.Application.Maps.Queries.GetCitiesList;

public interface ICitiesListQueryHandler
{
    Task<ErrorOr<List<CityQuery>>> GetCitiesByCountryIdAsync(Guid countryId);

    Task<ProfileCredentials> GetCitiesAndLanguagesAsync(Guid userId);
}