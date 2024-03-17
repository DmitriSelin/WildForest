using ErrorOr;
using WildForest.Application.Weather.Common.Models;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Weather;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast.Fabrics;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;

namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed class HomeWeatherForecastService : IHomeWeatherForecastService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWeatherForecastResponseFactory _weatherForecastResponseFactory;

    public HomeWeatherForecastService(
        IWeatherForecastResponseFactory weatherForecastResponseFactory,
        IUnitOfWork unitOfWork)
    {
        _weatherForecastResponseFactory = weatherForecastResponseFactory;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<List<WeatherForecastResponse>>> GetWeatherForecastsAsync(HomeWeatherForecastQuery query)
    {
        var userId = UserId.Create(query.UserId);
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var forecasts = (List<WeatherForecast>?)
            await _unitOfWork.WeatherForecastRepository.GetWeatherForecastsWithRatingByDateAsync(query.ForecastDate, user.CityId);

        if (forecasts is null || forecasts.Count == 0)
            return Errors.WeatherForecast.NotFound;

        var weatherForecasts = _weatherForecastResponseFactory.Create(forecasts);
        return weatherForecasts;
    }
}