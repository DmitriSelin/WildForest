using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Application.Marks.Common;

namespace WildForest.Application.Marks.Commands.LeaveComment;

public sealed class CommentCommandHandler : ICommentCommandHandler
{
    private readonly IMarkRepository _markRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherRepository;
    private readonly IMarkService _markService;

    public CommentCommandHandler(
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

    public async Task<ErrorOr<CommentDto>> LeaveCommentAsync(CommentCommand command)
    {
        var rating = Rating.Create(command.StarsCount);
        var comment = Comment.Create(command.Comment);
        var userId = UserId.Create(command.UserId);
        var weatherId = WeatherId.Create(command.WeatherId);

        var weatherForecast = await _weatherRepository.GetWeatherForecastByIdAsync(weatherId);

        if (weatherForecast is null)
            return Errors.WeatherForecast.NotFoundById;

        var user = await _userRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var mark = Mark.Create(rating, comment, userId, weatherId);

        await _markRepository.AddMarkAsync(mark);

        var weatherMark = await _markService.ChangeMediumMarkAsync(weatherForecast.Id, mark.Rating);

        if (weatherMark.IsError)
            return weatherMark.FirstError;

        double avgMark = weatherMark.Value.MediumMark.Value;

        return new CommentDto(
            mark.Id.Value,
            user.Id.Value,
            weatherForecast.Id.Value,
            mark.Date.Value,
            mark.Rating.Value,
            mark.Comment!.Value,
            avgMark);
    }
}