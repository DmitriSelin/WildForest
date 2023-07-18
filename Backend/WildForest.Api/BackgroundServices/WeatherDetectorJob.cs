using Quartz;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Commands.AddWeatherForecasts;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Api.BackgroundServices;

public sealed class WeatherDetectorJob : IJob
{
    private readonly ICityRepository _cityRepository;
    private readonly IWeatherForecastDbService _weatherForecastDbService;

    public WeatherDetectorJob(
        ICityRepository cityRepository,
        IWeatherForecastDbService weatherForecastDbService)
    {
        _cityRepository = cityRepository;
        _weatherForecastDbService = weatherForecastDbService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cities = (List<City>) await _cityRepository.GetDistinctCitiesFromUsersAsync();

        foreach (var city in cities)
        {
            await _weatherForecastDbService.AddWeatherForecastsInDbAsync(city.Id);//TODO:think about redone http client
        }
    }
}