using WildForest.Api.Services.Http.Weather;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Api.BackgroundServices
{
    public sealed class WeatherForecastBackgroundService : BackgroundService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IWeatherForecastHttpClient _httpClient;

        public WeatherForecastBackgroundService(ICityRepository cityRepository, IWeatherForecastHttpClient httpClient)
        {
            _cityRepository = cityRepository;
            _httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cities = await _cityRepository.GetCitiesByUsersAsync();

            foreach (var city in cities)
            {
                //var weatherForecast = await _httpClient.GetWeatherForecastAsync(city.Id);
            }
        }
    }
}