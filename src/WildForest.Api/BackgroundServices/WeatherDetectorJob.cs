using Quartz;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Application.Weather.Commands.AddWeatherForecasts;
using WildForest.Domain.Cities.Entities;

namespace WildForest.Api.BackgroundServices;

public sealed class WeatherDetectorJob : IJob
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWeatherForecastDbService _weatherForecastDbService;

    public WeatherDetectorJob(
        IUnitOfWork unitOfWork,
        IWeatherForecastDbService weatherForecastDbService)
    {
        _unitOfWork = unitOfWork;
        _weatherForecastDbService = weatherForecastDbService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cities = (List<City>) await _unitOfWork.CityRepository.GetDistinctCitiesFromUsersAsync();

        foreach (var city in cities)
        {
            await _weatherForecastDbService.AddWeatherForecastsInDbAsync(city.Id);
        }
    }
}