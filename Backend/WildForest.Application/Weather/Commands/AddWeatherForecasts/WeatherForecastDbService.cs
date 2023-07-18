using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;
using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public sealed class WeatherForecastDbService : IWeatherForecastDbService
{
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly IWeatherForecastFactory _weatherForecastFactory;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IThreeHourWeatherForecastRepository _threeHourWeatherForecastRepository;
    private readonly ICityRepository _cityRepository;

    public WeatherForecastDbService(
        ICityRepository cityRepository,
        IWeatherForecastHttpClient httpClient,
        IWeatherForecastFactory weatherForecastFactory,
        IWeatherForecastRepository weatherForecastRepository,
        IThreeHourWeatherForecastRepository threeHourWeatherForecastRepository)
    {
        _httpClient = httpClient;
        _weatherForecastFactory = weatherForecastFactory;
        _weatherForecastRepository = weatherForecastRepository;
        _threeHourWeatherForecastRepository = threeHourWeatherForecastRepository;
        _cityRepository = cityRepository;
    }

    public async Task AddWeatherForecastsInDbAsync(CityId cityId)
    {
        var city = await _cityRepository.GetCityByIdAsync(cityId);

        if (city is null)
            throw new ArgumentNullException(nameof(city));

        List<WeatherForecastVm>? forecasts = await _httpClient.GetWeatherForecastAsync(city);

        if (forecasts is null || forecasts.Count == 0)
            throw new ArgumentNullException(nameof(forecasts), "Error when getting weather forecasts");

        var weatherForecasts = _weatherForecastFactory.Create(forecasts, cityId);

        await _weatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts.Item1);
        await _threeHourWeatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts.Item2);
    }
}