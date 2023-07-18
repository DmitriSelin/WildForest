using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Weather;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Infrastructure.Weather;

public sealed class ExistingWeatherDataService : IExistingWeatherDataService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IWeatherForecastHttpClient _weatherForecastHttpClient;

    public ExistingWeatherDataService(
        IWeatherForecastRepository weatherForecastRepository,
        IWeatherForecastHttpClient weatherForecastHttpClient)
    {
        _weatherForecastRepository = weatherForecastRepository;
        _weatherForecastHttpClient = weatherForecastHttpClient;
    }

    public async Task CheckWeatherDataExisting(CityId cityId)
    {
        //var forecasts = (List<ThreeHourWeatherForecast>?) await _weatherForecastRepository.GetWeatherForecastsByCityIdAsync(cityId);

        //if (forecasts is null || forecasts.Count == 0)
        //{
        //    var weatherForecasts = await _weatherForecastHttpClient.GetWeatherForecastAsync(cityId);
        //    await _weatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts);
        //}
    }
}