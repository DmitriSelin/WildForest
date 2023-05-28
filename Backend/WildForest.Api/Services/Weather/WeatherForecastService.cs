using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Api.Services.Weather;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastService(
        IWeatherForecastHttpClient httpClient,
        ICityRepository cityRepository,
        IWeatherForecastRepository weatherForecastRepository)
    {
        _httpClient = httpClient;
        _weatherForecastRepository = weatherForecastRepository;
        _cityRepository = cityRepository;
    }

    public async Task AddWeatherForecastsToDb()
    {
        var cities = (List<City>) await _cityRepository.GetDistinctCitiesFromUsersAsync();

        foreach (var city in cities)
        {
            var forecasts = await _httpClient.GetWeatherForecastAsync(city.Id);
            await _weatherForecastRepository.AddWeatherForecastsAsync(forecasts);
        }
    }
}