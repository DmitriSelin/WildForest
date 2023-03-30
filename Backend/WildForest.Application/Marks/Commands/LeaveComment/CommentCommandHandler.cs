using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Marks.Common;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Marks.Commands.LeaveComment;

public sealed class CommentCommandHandler : ICommentCommandHandler
{
    private readonly IMarkRepository _markRepository;
    private readonly IUserRepository _userRepository;
    private readonly IWeatherForecastRepository _weatherRepository;

    public CommentCommandHandler(
        IMarkRepository markRepository,
        IUserRepository userRepository,
        IWeatherForecastRepository weatherRepository)
    {
        _markRepository = markRepository;
        _userRepository = userRepository;
        _weatherRepository = weatherRepository;
    }

    public async Task<ErrorOr<MarkDto>> LeaveCommentAsync(CommentCommand command)
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

        return new MarkDto(
            mark.Id.Value,
            user.Id.Value,
            weatherForecast.Id.Value,
            mark.Date.Value,
            mark.Rating.Value,
            mark.Comment?.Value);
    }
}