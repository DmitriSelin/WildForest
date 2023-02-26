using WildForest.Api.Services.Http.Weather;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;

namespace WildForest.Api.Services.Weather;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly IWeatherForecastHttpClient _httpClient;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastService(
        IWeatherForecastHttpClient httpClient, 
        IUserRepository userRepository,
        IWeatherForecastRepository weatherForecastRepository)
    {
        _httpClient = httpClient;
        _userRepository = userRepository;
        _weatherForecastRepository = weatherForecastRepository;
    }

    public async Task AddWeatherForecastsToDb()
    {
        var users = await _userRepository.GetAllUsersWithDistinctCities();

        foreach (var user in users)
        {
            var forecasts = await _httpClient.GetWeatherForecastAsync(user.City.Id);
            await _weatherForecastRepository.AddWeatherForecastsAsync(forecasts);
        }
    }
}