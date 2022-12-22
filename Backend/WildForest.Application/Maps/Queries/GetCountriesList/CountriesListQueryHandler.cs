using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Maps.Queries.GetCountriesList
{
    public class CountriesListQueryHandler : ICountriesListQueryHandler
    {
        private readonly ICountryRepository _countryRepository;

        public CountriesListQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _countryRepository.GetAllCountriesAsync();
        }
    }
}