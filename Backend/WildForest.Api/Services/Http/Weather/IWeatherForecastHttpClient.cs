using WildForest.Application.Weather.Common;

namespace WildForest.Api.Services.Http.Weather
{
    public interface IWeatherForecastHttpClient
    {
        Task<List<WeatherForecastDto>> GetWeatherForecastAsync(Guid cityId, DateTime date);
    }
}