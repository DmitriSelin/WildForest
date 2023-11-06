using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;
using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public sealed class WeatherForecastDbService : IWeatherForecastDbService
{
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly IWeatherForecastFactory _weatherForecastFactory;
    private readonly IUnitOfWork _unitOfWork;

    public WeatherForecastDbService(
        IWeatherForecastHttpClient httpClient,
        IWeatherForecastFactory weatherForecastFactory,
        IUnitOfWork unitOfWork)
    {
        _httpClient = httpClient;
        _weatherForecastFactory = weatherForecastFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task AddWeatherForecastsInDbAsync(CityId cityId)
    {
        var city = await _unitOfWork.CityRepository.GetCityByIdAsync(cityId);

        if (city is null)
            throw new ArgumentNullException(nameof(city));

        List<WeatherForecastVm>? forecasts = await _httpClient.GetWeatherForecastAsync(city);

        if (forecasts is null || forecasts.Count == 0)
            throw new ArgumentNullException(nameof(forecasts), "Error when getting weather forecasts");

        var weatherForecasts = _weatherForecastFactory.Create(forecasts, cityId);

        await _unitOfWork.WeatherForecastRepository.AddWeatherForecastsAsync(weatherForecasts);
        await _unitOfWork.SaveChangesAsync();
    }
}