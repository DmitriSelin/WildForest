using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Comments.Queries.GetComments;

public sealed class CommentQueryHandler : ICommentQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<IEnumerable<CommentDto>>> GetCommentsAsync(WeatherForecastId weatherForecastId)
    {
        var comments = await _unitOfWork.CommentRepository.GetCommentsByWeatherForecastIdWithUsersAsync(weatherForecastId);

        if (comments is null || !comments.Any())
            return Errors.Comment.NotFoundByWeatherForecastId;

        var commentsDto = comments
            .Select(x => new CommentDto(x.Id.Value, x.Text, x.Date, $"{x.User.LastName} {x.User.FirstName}"))
            .ToList();

        return commentsDto;
    }
}