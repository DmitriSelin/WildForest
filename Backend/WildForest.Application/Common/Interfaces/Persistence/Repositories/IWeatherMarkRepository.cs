using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IWeatherMarkRepository
{
    Task AddWeatherMarkAsync(WeatherMark weatherMark);

    Task<WeatherMark?> GetWeatherMarkByWeatherIdAsync(WeatherId weatherId);

    Task UpdateWeatherMarkAsync(WeatherMark weatherMark);
}