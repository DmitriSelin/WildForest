using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId, DateTime date);
    }
}