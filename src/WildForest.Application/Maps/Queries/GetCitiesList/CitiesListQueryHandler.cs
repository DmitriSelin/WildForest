using ErrorOr;
using MapsterMapper;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Countries.ValueObjects;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Application.Maps.Queries.GetCitiesList.Dto;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Common.Models;

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

        public async Task<ProfileCredentials> GetCitiesAndLanguagesAsync(Guid userId)
        {
            var id = UserId.Create(userId);
            var languages = await _unitOfWork.LanguageRepository.GetAllLanguagesAsync();
            var cities = await _unitOfWork.CityRepository.GetCitiesByUserIdAsync(id);

            var citiesDto = cities.Select(x => new NamedDto(x.Id.Value, x.Name.Value));
            var languagesDto = languages.Select(x => new NamedDto(x.Id.Value, x.Name));

            var profileCredentials = new ProfileCredentials(citiesDto, languagesDto);
            return profileCredentials;
        }

        public async Task<ErrorOr<List<CityQuery>>> GetCitiesByCountryIdAsync(Guid countryId)
        {
            var id = CountryId.Create(countryId);

            var cities = (List<City>?) await _unitOfWork.CityRepository.GetCitiesByCountryIdAsync(id);

            if (cities is null || !cities.Any())
            {
                return Errors.City.NotFoundCitiesByCountry;
            }

            return _mapper.Map<List<CityQuery>>(cities);
        }
    }
}