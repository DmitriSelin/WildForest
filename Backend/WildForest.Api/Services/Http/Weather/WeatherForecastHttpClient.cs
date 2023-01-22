using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Domain.Cities.Entities;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Cities.ValueObjects;
using System.Text.Json;

namespace WildForest.Api.Services.Http.Weather
{
    public class WeatherForecastHttpClient : IWeatherForecastHttpClient
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

            string? baseUrl = _configuration["WeatherForecast:BaseWeatherForecastUrl"];

            if (baseUrl is null)
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<List<WeatherForecastDto>> GetWeatherForecastAsync(Guid cityId, DateTime date)
        {
            var city = await _cityRepository.GetCityByIdAsync(CityId.CreateCityId(cityId));

            if (city is null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            double lat = city.Location.Latitude;
            double lon = city.Location.Longitude;
            var exclude = "daily";   //Part of the day
            string? appid = _configuration["WeatherForecast:ApiKey"];

            var url = $"?lat={lat}&lon={lon}&exclude={exclude}&appid={appid}";

            string weatherResponse = await _httpClient.GetStringAsync(url);

            List<WeatherForecastDto> weather = JsonSerializer.DeserializeAsync<List<WeatherForecastDto>>(weatherResponse);
        }
    }
}