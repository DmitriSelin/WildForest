using ErrorOr;
using WildForest.Application.Comments.Common;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Comments.ValueObjects;
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

    public async Task<IEnumerable<CommentDto>> GetCommentsAsync(WeatherForecastId weatherForecastId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsByWeatherForecastIdWithUsersAsync(weatherForecastId);

        if (comments is null || !comments.Any())
            return Array.Empty<CommentDto>();

        var commentsDto = comments
            .Select(x => new CommentDto(x.Id.Value, x.Text, x.Date, x.User.FirstName.Value, x.User.LastName.Value, x.User.Image))
            .ToArray();

        return commentsDto;
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

        return new CommentDto(comment.Id.Value, comment.Text, comment.Date, user.FirstName.Value, user.LastName.Value, user.Image);
    }

    public async Task<ErrorOr<CommentDto>> UpdateCommentAsync(CommentCommandForUpdate command)
    {
        var userId = UserId.Create(command.UserId);
        var user = await _unitOfWork.UserRepository.GetUserByIdAsync(userId);

        if (user is null)
            return Errors.User.NotFoundById;

        var commentId = CommentId.Create(command.Id);
        var weatherForecastId = WeatherForecastId.Create(command.WeatherForecastId);

        var comment = await _unitOfWork.CommentRepository.GetCommentByIdAndUserIdAndWeatherForecastIdAsync(commentId, user.Id, weatherForecastId);

        if (comment is null)
            return Errors.Comment.NotFound;

        comment.Update(command.NewText);

        await _unitOfWork.SaveChangesAsync();

        return new CommentDto(comment.Id.Value, comment.Text, comment.Date,
            user.FirstName.Value, user.LastName.Value, user.Image);
    }
}