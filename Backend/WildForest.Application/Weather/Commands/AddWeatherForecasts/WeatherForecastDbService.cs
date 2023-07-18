using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public sealed class WeatherForecastDbService : IWeatherForecastDbService
{
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly IWeatherForecastFactory _weatherForecastFactory;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IThreeHourWeatherForecastRepository _threeHourWeatherForecastRepository;

    public WeatherForecastDbService(
        IWeatherForecastHttpClient httpClient,
        IWeatherForecastFactory weatherForecastFactory,
        IWeatherForecastRepository weatherForecastRepository,
        IThreeHourWeatherForecastRepository threeHourWeatherForecastRepository)
    {
        _httpClient = httpClient;
        _weatherForecastFactory = weatherForecastFactory;
        _weatherForecastRepository = weatherForecastRepository;
        _threeHourWeatherForecastRepository = threeHourWeatherForecastRepository;
    }

    public async Task AddWeatherForecastsInDbAsync(CityId cityId)
    {
        var forecasts = await _httpClient.GetWeatherForecastAsync(cityId);

        if (forecasts is null || forecasts.Count == 0)
            throw new ArgumentNullException(nameof(forecasts), "Error when getting weather forecasts");

        var weatherForecasts = _weatherForecastFactory.Create(forecasts, cityId);

        await _weatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts.Item1);
        await _threeHourWeatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts.Item2);
    }
}