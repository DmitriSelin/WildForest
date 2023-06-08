using System.Threading.Tasks;
using WildForest.Frontend.Contracts.Weather;

namespace WildForest.Frontend.Services.Weather.Interfaces;

/// <summary>
/// Service for getting weather forecast
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Method for getting today weather forecast
    /// </summary>
    /// <param name="token">Jwt token</param>
    /// <returns>WeatherResponseBase</returns>
    Task<WeatherResponseBase> GetTodayWeatherForecastAsync(string token);
}