using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Weather;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast.Fabrics;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed class HomeWeatherForecastService : IHomeWeatherForecastService
{
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherForecastRepository;
    private readonly IWeatherForecastResponseFactory _weatherForecastResponseFactory;

    public HomeWeatherForecastService(
        IUserRepository userRepository,
        IWeatherForecastRepository weatherForecastRepository,
        IWeatherForecastResponseFactory weatherForecastResponseFactory)
    {
        _userRepository = userRepository;
        _weatherForecastRepository = weatherForecastRepository;
        _weatherForecastResponseFactory = weatherForecastResponseFactory;
    }

    public async Task<ErrorOr<List<WeatherForecastResponse>>> GetWeatherForecastsAsync(HomeWeatherForecastQuery query)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var forecasts = (List<WeatherForecast>?)
            await _weatherForecastRepository.GetWeatherForecastsWithVoteByDateAsync(query.ForecastDate, user.CityId);

        if (forecasts is null || forecasts.Count == 0)
            return Errors.WeatherForecast.NotFound;

        var weatherForecasts = _weatherForecastResponseFactory.Create(forecasts);
        return weatherForecasts;
    }
}