﻿using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Weather.Queries.GetWeatherForecast
{
    public sealed class WeatherForecastDetector : IWeatherForecastDetector
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IWeatherForecastRepository _dayWeatherRepository;

        public WeatherForecastDetector(
            IUserRepository userRepository,
            ICityRepository cityRepository,
            IWeatherForecastRepository dayWeatherRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
            _dayWeatherRepository = dayWeatherRepository;
        }

        public async Task<ErrorOr<List<WeatherForecastDto>>> GetWeatherForecast(ForecastQuery query)
        {
            var userId = UserId.Create(query.UserId);
            var cityId = CityId.Create(query.CityId);

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

            var forecastDate = ForecastDate.Create(query.ForecastDate);

            List<WeatherForecast>? weather = await _dayWeatherRepository.GetWeatherForecastAsync(city.Id, forecastDate);

            if (weather is null)
            {
                return Errors.Weather.NotFound;
            }

            return new();
        }
    }
}