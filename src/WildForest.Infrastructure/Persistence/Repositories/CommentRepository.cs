using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Comments.Entities;
using WildForest.Domain.Comments.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class CommentRepository : ICommentRepository
{
    private readonly WildForestDbContext _context;

    public CommentRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<Comment?> GetCommentByIdAndUserIdAndWeatherForecastIdAsync(CommentId commentId, UserId userId, WeatherForecastId weatherForecastId)
    {
        return await _context.Comments
            .FirstOrDefaultAsync(x => x.Id == commentId && x.UserId == userId && x.WeatherForecastId == weatherForecastId);
    }

    public async Task<Comment?> GetCommentByIdAsync(CommentId commentId)
    {
        return await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> GetCommentsByWeatherForecastIdWithUsersAsync(WeatherForecastId weatherForecastId)
    {
        return await _context.Comments
            .Include(x => x.User)
            .Where(x => x.WeatherForecastId == weatherForecastId)
            .ToListAsync();
    }
}