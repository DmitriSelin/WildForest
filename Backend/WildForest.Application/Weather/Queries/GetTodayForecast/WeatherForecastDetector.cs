using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Common.Exceptions;
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

        public async Task<ErrorOr<List<WeatherForecust>>> GetTodayWeatherForecast(TodayForecastQuery query)
        {
            var user = await _userRepository.GetUserByIdAsync(query.UserId);

            if (user is null)
            {
                return Errors.User.NotFoundById;
            }

            var city = await _cityRepository.GetCityByIdAsync(query.CityId);

            if (city is null)
            {
                return Errors.City.NotFoundById;
            }

            List<DayWeather>? todayWeather = await _dayWeatherRepository.GetWeatherAsync(city.Id, DateTime.Now.Date);

            if (todayWeather is null)
            {
                return Errors.Weather.NotFound;
            }

            return new List<WeatherForecust>();
        }
    }
}