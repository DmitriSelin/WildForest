using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Exceptions;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public sealed class WeatherForecastDetector : IWeatherForecastDetector
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IDayWeatherRepository _dayWeatherRepository;

        public WeatherForecastDetector(
            IUserRepository userRepository,
            ICityRepository cityRepository,
            IDayWeatherRepository dayWeatherRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _dayWeatherRepository = dayWeatherRepository;
        }

        public async Task<ErrorOr<List<WeatherForecust>>> GetTodayWeatherForecast(ForecastQuery query)
        {
            var userId = UserId.CreateUserId(query.UserId);
            var cityId = CityId.CreateCityId(query.CityId);

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user is null)
            {
                return Errors.User.NotFoundById;
            }

            var city = await _cityRepository.GetCityByIdAsync(cityId);

            if (city is null)
            {
                return Errors.City.NotFoundById;
            }

            List<DayWeather>? todayWeather = await _dayWeatherRepository.GetWeatherAsync(city.Id, DateTime.Now.Date);

            if (todayWeather is null)
            {
                return Errors.Weather.NotFound;
            }

            return new();
        }
    }
}