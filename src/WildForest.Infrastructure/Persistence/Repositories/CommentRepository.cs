using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Comments.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(WildForestDbContext context) : base(context) { }

    public async Task<Comment?> GetCommentByIdAndUserIdAndWeatherForecastIdAsync(CommentId commentId, UserId userId, WeatherForecastId weatherForecastId)
    {
        return await Context.Comments
            .FirstOrDefaultAsync(x => x.Id == commentId && x.UserId == userId && x.WeatherForecastId == weatherForecastId);
    }

    public async Task<Comment?> GetCommentByIdAsync(CommentId commentId)
    {
        return await Context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByWeatherForecastIdWithUsersAsync(WeatherForecastId weatherForecastId)
    {
        return await Context.Comments
            .Include(x => x.User)
            .Where(x => x.WeatherForecastId == weatherForecastId)
            .ToListAsync();
    }
}