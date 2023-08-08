using MapsterMapper;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Countries.Entities;

namespace WildForest.Application.Maps.Queries.GetCountriesList;

public sealed class CountriesListQueryHandler : ICountriesListQueryHandler
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountriesListQueryHandler(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<List<CountryQuery>> GetCountriesAsync()
    {
        var countries = (List<Country>)await _countryRepository.GetAllCountriesAsync();

        return _mapper.Map<List<CountryQuery>>(countries);
    }
}