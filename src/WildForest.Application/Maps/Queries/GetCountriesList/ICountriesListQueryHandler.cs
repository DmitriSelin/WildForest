namespace WildForest.Application.Maps.Queries.GetCountriesList;

public interface ICountriesListQueryHandler
{
    Task<List<CountryQuery>> GetCountriesAsync();
}