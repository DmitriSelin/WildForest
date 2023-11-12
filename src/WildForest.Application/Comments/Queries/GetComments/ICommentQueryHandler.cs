using ErrorOr;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Comments.Queries.GetComments;

public interface ICommentQueryHandler
{
    Task<ErrorOr<IEnumerable<CommentDto>>> GetCommentsAsync(WeatherForecastId weatherForecastId);
}