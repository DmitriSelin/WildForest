using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Api.Services.Http.Weather
{
    public interface IWeatherForecastHttpClient
    {
        Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId);
    }
}