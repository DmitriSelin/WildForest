using Microsoft.EntityFrameworkCore;
using WildForest.Api.Services.Http.Weather;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Api.BackgroundServices
{
    public sealed class WeatherForecastBackgroundService : BackgroundService
    {
        private readonly WildForestDbContext _context;
        private readonly IWeatherForecastHttpClient _httpClient;

        public WeatherForecastBackgroundService(IWeatherForecastHttpClient httpClient, WildForestDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cities = await _context.Cities
                .Include(x => x.Users)
                .ToListAsync();

            foreach (var city in cities)
            {
                var weatherForecast = await _httpClient.GetWeatherForecastAsync(city.Id);

                await _context.WeatherForecasts.AddRangeAsync(weatherForecast);
            }
        }
    }
}