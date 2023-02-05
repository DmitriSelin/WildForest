﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId, ForecastDate forecastDate);
    }
}