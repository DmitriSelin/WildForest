using System;
using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Weather;

namespace WildForest.Frontend.Services.Weather.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponseBase> GetTodayWeatherForecastAsync(string token);
    }
}