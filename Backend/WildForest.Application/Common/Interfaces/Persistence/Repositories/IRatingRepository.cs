using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IRatingRepository
{
    Task AddRatingAsync(Rating rating);

    Task<Rating?> GetRatingByIdWithVotesByUserIdAsync(RatingId ratingId, UserId userId);

    Task SaveChangesAsync();
}