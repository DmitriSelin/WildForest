using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Weather;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Weather;

public sealed class ExistingWeatherDataService : IExistingWeatherDataService
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IWeatherForecastHttpClient _httpClient;

    public ExistingWeatherDataService(IWeatherForecastRepository weatherForecastRepository)
    {
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task CheckWeatherDataExisting(CityId cityId)
    {
        var forecasts = (List<WeatherForecast>?) await _weatherForecastRepository.GetWeatherForecastsByCityIdAsync(cityId);

        if (forecasts is null || forecasts.Count == 0)
        {

        }
    }
}