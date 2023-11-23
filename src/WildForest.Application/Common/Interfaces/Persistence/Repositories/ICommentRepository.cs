using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Comments.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface ICommentRepository
{
    Task AddCommentAsync(Comment comment);

    Task<IEnumerable<Comment>> GetCommentsByWeatherForecastIdWithUsersAsync(WeatherForecastId weatherForecastId);

    Task<Comment?> GetCommentByIdAsync(CommentId commentId);

    Task<Comment?> GetCommentByIdAndUserIdAndWeatherForecastIdAsync(CommentId commentId, UserId userId, WeatherForecastId weatherForecastId);
}