using ErrorOr;
using MapsterMapper;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Application.Maps.Queries.GetCitiesList
{
    public sealed class CitiesListQueryHandler : ICitiesListQueryHandler
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CitiesListQueryHandler(ICityRepository cityRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<CityQuery>>> GetCitiesByCountryIdAsync(Guid countryId)
        {
            var id = CountryId.CreateCountryId(countryId);

            var cities = (List<City>?) await _cityRepository.GetCitiesByCountryIdAsync(id);

            if (cities is null)
            {
                return Errors.City.NotFoundCitiesByCountry;
            }

            return _mapper.Map<List<CityQuery>>(cities);
        }
    }
}