using ErrorOr;
using WildForest.Application.Comments.Common;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Comments.Commands.Services;

public sealed class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CommentDto>> AddCommentAsync(CommentCommand command)
    {
        var userId = UserId.Create(command.UserId);
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var weatherForecastId = WeatherForecastId.Create(command.WeatherForecastId);
        var weather = await _unitOfWork.WeatherForecastRepository.GetWeatherForecastByIdAsync(weatherForecastId);

        if (weather is null)
            return Errors.WeatherForecast.NotFoundById;

        var comment = Comment.Create(command.Text, userId, weatherForecastId);
        await _unitOfWork.CommentRepository.AddCommentAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return new CommentDto(comment.Id.Value, command.Text, comment.Date, $"{user.LastName} {user.FirstName}");
    }
}