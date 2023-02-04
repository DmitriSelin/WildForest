using WildForest.Application.Common.Interfaces.Persistence;
using WildForest.Application.Weather.Common;
using WildForest.Domain.Cities.ValueObjects;

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

        public async Task<List<WeatherForecastDto>> GetWeatherForecastAsync(Guid cityId)
        {
            var city = await _cityRepository.GetCityByIdAsync(CityId.CreateCityId(cityId));

            if (city is null)
            {
                throw new ArgumentNullException(nameof(city));
            }

            string lat = city.Location.Latitude.ToString();
            string lon = city.Location.Longitude.ToString();
            string units = "metric";
            string? appid = _configuration["WeatherForecast:ApiKey"];

            var url = $"?lat={lat}&lon={lon}&units={units}&appid={appid}";

            string response = await _httpClient.GetStringAsync(url);

            return new();
        }
    }
}