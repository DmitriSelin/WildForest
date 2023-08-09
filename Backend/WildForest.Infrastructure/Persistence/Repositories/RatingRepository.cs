using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class RatingRepository : IRatingRepository
{
    private readonly WildForestDbContext _context;

    public RatingRepository(WildForestDbContext context)
    {
        _context = context;
    }

    public async Task AddRatingAsync(Rating rating)
    {
        await _context.Ratings.AddAsync(rating);
    }

    public async Task<Rating?> GetRatingByIdWithVotesByUserIdAsync(RatingId ratingId, UserId userId)
    {
        return await _context.Ratings
            .Include(x => x.Votes.Where(a => a.UserId == userId))
            .SingleOrDefaultAsync(x => x.Id == ratingId);
    }

    public async Task<Vote?> GetVoteByIdAndUserIdWithRatingAsync(RatingId ratingId, UserId userId, VoteId voteId)
    {
        return await _context.Votes
            .Include(x => x.Rating)
            .SingleOrDefaultAsync(
                x => x.Id == voteId
                && x.UserId == userId
                && x.RatingId == ratingId);
    }
}