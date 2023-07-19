﻿using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherForecastRepository
{
    Task AddWeatherForecastsAsync(IEnumerable<WeatherForecast> weatherForecasts);

    Task<IEnumerable<WeatherForecast>> GetWeatherForecastsWithVoteByDateAsync(
        DateOnly date, CityId cityId);
}