using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json;
using WildForest.Application.Common.Interfaces.Http;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Infrastructure.Common.Extensions;
using WildForest.Infrastructure.Http.JsonConverters;

namespace WildForest.Infrastructure.Http;

public sealed class WeatherForecastHttpClient : IWeatherForecastHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ICityRepository _cityRepository;

    public WeatherForecastHttpClient(
        HttpClient httpClient,
        IConfiguration configuration,
        ICityRepository cityRepository)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _cityRepository = cityRepository;

        var baseUrl = _configuration["WeatherForecast:BaseWeatherForecastUrl"];

        if (baseUrl is null)
        {
            throw new ArgumentNullException(nameof(baseUrl));
        }

        _httpClient.BaseAddress = new Uri(baseUrl);
    }

    public async Task<List<WeatherForecast>> GetWeatherForecastAsync(CityId cityId)
    {
        var city = await _cityRepository.GetCityByIdAsync(cityId);

        if (city is null)
        {
            throw new ArgumentNullException(nameof(city));
        }

        string lat = city.Location.Latitude
            .ToString()
            .ReplaceCommaByPeriod();

        string lon = city.Location.Longitude
            .ToString()
            .ReplaceCommaByPeriod();

        var appid = _configuration["WeatherForecast:ApiKey"];

        var url = $"?lat={lat}&lon={lon}&units=metric&appid={appid}";

        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new WeatherForecastConverter(city.Id));

        var weatherForecast = await _httpClient.GetFromJsonAsync<List<WeatherForecast>>(url, jsonOptions);

        if (weatherForecast is null)
        {
            throw new ArgumentNullException(nameof(weatherForecast));
        }

        return weatherForecast;
    }
}