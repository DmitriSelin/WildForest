using WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRatingRepository : IRepository<Rating>
{
    Task<Rating?> GetRatingByIdWithVotesByUserIdAsync(RatingId ratingId, UserId userId);

    Task<Vote?> GetVoteByIdAndUserIdWithRatingAsync(RatingId ratingId, UserId userId, VoteId voteId);
}