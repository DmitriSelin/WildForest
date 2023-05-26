using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using WildForest.Frontend.Common;
using WildForest.Frontend.Contracts.Authentication;
using WildForest.Frontend.Contracts.Weather;
using WildForest.Frontend.Services.Weather.Interfaces;
using WildForest.Frontend.Services.Weather.JsonConverters;

namespace WildForest.Frontend.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponseBase> GetTodayWeatherForecastAsync(string token)
        {
            var date = DateOnly.FromDateTime(DateTime.Now.Date);

            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateOnlyConverter());

            var forecastDate = JsonSerializer.Serialize(date, options);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"{ApiItemKeys.BaseUrl}/weather/forecast/homeCity/{forecastDate}");

            string body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var weatherForecast = JsonSerializer.Deserialize<WeatherForecastVm>(body);
                return new(weatherForecast, (int)response.StatusCode, null);
            }

            var badResponse = JsonSerializer.Deserialize<BadResponse>(body);
            return new(null, badResponse!.Status, badResponse.Title);
        }
    }
}