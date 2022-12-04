using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence
{
    public interface IDayWeatherRepository
    {
        Task<List<DayWeather>> GetWeatherAsync(CityId cityId, DateTime date);
    }
}