using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherForecastRepository
{
    Task AddWeatherForecastsAsync(IEnumerable<ThreeHourWeatherForecast> forecasts);

    Task<ThreeHourWeatherForecast?> GetWeatherForecastByIdAsync(WeatherForecastId weatherId);
}