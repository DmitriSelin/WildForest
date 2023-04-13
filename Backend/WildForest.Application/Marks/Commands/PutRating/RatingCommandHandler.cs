using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.Marks.Entities;
using WildForest.Application.Marks.Common;
using WildForest.Application.Common.Interfaces.Services;

namespace WildForest.Application.Marks.Commands.PutRating;

public sealed class RatingCommandHandler : IRatingCommandHandler
{
    private readonly IMarkRepository _markRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherRepository;
    private readonly IMarkService _markService;

    public RatingCommandHandler(
        IMarkRepository markRepository,
        IUserRepository userRepository,
        IWeatherForecastRepository weatherRepository,
        IMarkService markService)
    {
        _markRepository = markRepository;
        _userRepository = userRepository;
        _weatherRepository = weatherRepository;
        _markService = markService;
    }
    public async Task<ErrorOr<RatingDto>> PutRatingAsync(RatingCommand command)
    {
        var rating = Rating.Create(command.Rating);
        var userId = UserId.Create(command.UserId);
        var weatherId = WeatherId.Create(command.WeatherId);

        var weatherForecast = await _weatherRepository.GetWeatherForecastByIdAsync(weatherId);

        if (weatherForecast is null)
            return Errors.WeatherForecast.NotFoundById;

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var mark = Mark.Create(rating, null, userId, weatherId);

        await _markRepository.AddMarkAsync(mark);

        var weatherMark = await _markService.ChangeMediumMarkAsync(weatherForecast.Id, mark.Rating);

        if (weatherMark.IsError)
            return weatherMark.FirstError;

        double avgMark = weatherMark.Value.MediumMark.Value;

        return new RatingDto(
            mark.Id.Value,
            user.Id.Value,
            weatherForecast.Id.Value,
            mark.Date.Value,
            mark.Rating.Value,
            avgMark);
    }
}