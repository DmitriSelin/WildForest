using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Maps.Common;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Maps.Queries.GetCountriesList
{
    public class CountriesListQueryHandler : ICountriesListQueryHandler
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountriesListQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        public async Task<List<CountryViewModel>> GetCountriesAsync()
        {
            var countries = (List<Country>) await _countryRepository.GetAllCountriesAsync();

            return _mapper.Map<List<CountryViewModel>>(countries);
        }
    }
}