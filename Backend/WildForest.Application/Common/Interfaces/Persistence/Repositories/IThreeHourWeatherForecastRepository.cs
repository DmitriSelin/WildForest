using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IThreeHourWeatherForecastRepository
{
    Task AddWeatherForecastsAsync(IEnumerable<ThreeHourWeatherForecast> weatherForecasts);
}