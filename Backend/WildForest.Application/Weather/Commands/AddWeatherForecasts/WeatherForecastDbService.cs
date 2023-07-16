using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public sealed class WeatherForecastDbService : IWeatherForecastDbService
{
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherForecastHttpClient _httpClient;

    public WeatherForecastDbService(ICityRepository cityRepository, IWeatherForecastHttpClient httpClient)
    {
        _cityRepository = cityRepository;
        _httpClient = httpClient;
    }

    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public async Task AddWeatherForecastsInDbAsync()
    {
        
    }
}