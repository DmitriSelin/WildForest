using Microsoft.EntityFrameworkCore;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Infrastructure.Persistence.Context;
using WildForest.Infrastructure.Persistence.Repositories.Base;

namespace WildForest.Infrastructure.Persistence.Repositories;

public sealed class RatingRepository : Repository<Rating>, IRatingRepository
{
    public RatingRepository(WildForestDbContext context) : base(context) { }

    public async Task<Rating?> GetRatingByIdWithVotesByUserIdAsync(RatingId ratingId, UserId userId)
    {
        return await Context.Ratings
            .Include(x => x.Votes.Where(a => a.UserId == userId))
            .SingleOrDefaultAsync(x => x.Id == ratingId);
    }

    public async Task<Vote?> GetVoteByIdAndUserIdWithRatingAsync(RatingId ratingId, UserId userId, VoteId voteId)
    {
        return await Context.Votes
            .Include(x => x.Rating)
            .SingleOrDefaultAsync(
                x => x.Id == voteId
                && x.UserId == userId
                && x.RatingId == ratingId);
    }
}