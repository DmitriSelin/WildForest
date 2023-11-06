using ErrorOr;
using MapsterMapper;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;

namespace WildForest.Application.Maps.Queries.GetCitiesList
{
    public sealed class CitiesListQueryHandler : ICitiesListQueryHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CitiesListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ErrorOr<List<CityQuery>>> GetCitiesByCountryIdAsync(Guid countryId)
        {
            var id = CountryId.Create(countryId);

            var cities = (List<City>?) await _unitOfWork.CityRepository.GetCitiesByCountryIdAsync(id);

            if (cities is null)
            {
                return Errors.City.NotFoundCitiesByCountry;
            }

            return _mapper.Map<List<CityQuery>>(cities);
        }
    }
}