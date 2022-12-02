using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Exceptions;
using WildForest.Domain.Users.ValueObjects;
using User = WildForest.Domain.Users.Entities.User;

namespace WildForest.Infrastructure.Services
{
    public class CityService : ICityService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;

        public CityService(IUserRepository userRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ErrorOr<CityId>> GetCityIdAsync(UserId userId)
        {
            User? user = await _userRepository.GetUserByIdAsync(userId);
            
            if (user is null)
            {
                return Errors.User.NotFoundById;
            }

            City? city = await _cityRepository.GetCityByIdAsync(user.CityId);

            if (city is null)
            {
                return Errors.City.NotFoundById;
            }

            return city.Id;
        }
    }
}