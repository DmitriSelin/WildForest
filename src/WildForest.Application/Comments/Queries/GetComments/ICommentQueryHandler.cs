using ErrorOr;
using WildForest.Application.Comments.Common;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Comments.Queries.GetComments;

public interface ICommentQueryHandler
{
    Task<ErrorOr<IEnumerable<CommentDto>>> GetCommentsAsync(WeatherForecastId weatherForecastId);
}