using Quartz;
using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Api.BackgroundServices;

public sealed class WeatherDetectorJob : IJob
{
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherDetectorJob(
        ICityRepository cityRepository,
        IWeatherForecastHttpClient httpClient,
        IWeatherForecastRepository weatherForecastRepository)
    {
        _cityRepository = cityRepository;
        _httpClient = httpClient;
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cities = (List<City>) await _cityRepository.GetDistinctCitiesFromUsersAsync();

        foreach (var city in cities)
        {
            var forecasts = await _httpClient.GetWeatherForecastAsync(city.Id);
            await _weatherForecastRepository.AddWeatherForecastsAsync(forecasts);
        }
    }
}