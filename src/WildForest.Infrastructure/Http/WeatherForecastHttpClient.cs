using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.Entities;
using WildForest.Infrastructure.Common.Extensions;
using WildForest.Infrastructure.Http.Builders;
using WildForest.Infrastructure.Http.JsonConverters;

namespace WildForest.Infrastructure.Http;

public sealed class WeatherForecastHttpClient : IWeatherForecastHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IWeatherForecastBuilder _weatherForecastBuilder;

    public WeatherForecastHttpClient(
        HttpClient httpClient,
        IConfiguration configuration,
        IWeatherForecastBuilder weatherForecastBuilder)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _weatherForecastBuilder = weatherForecastBuilder;

        var baseUrl = _configuration["WeatherForecast:BaseWeatherForecastUrl"];

        if (baseUrl is null)
            throw new ArgumentNullException(nameof(baseUrl));

        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<List<WeatherForecastVm>> GetWeatherForecastAsync(City city)
    {
        string lat = city.Location.Latitude
            .ToString()
            .ReplaceCommaByPeriod();

        string lon = city.Location.Longitude
            .ToString()
            .ReplaceCommaByPeriod();

        var appid = _configuration["WeatherForecast:ApiKey"];

        var url = $"?lat={lat}&lon={lon}&units=metric&appid={appid}";

        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new WeatherForecastConverter(_weatherForecastBuilder));

        var weatherForecasts = await _httpClient.GetFromJsonAsync<List<WeatherForecastVm>>(url, jsonOptions);

        if (weatherForecasts is null)
            throw new ArgumentNullException(nameof(weatherForecasts));

        return weatherForecasts;
    }
}