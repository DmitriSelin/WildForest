using WildForest.Dto.Models;

namespace WildForest.Contracts.Ratings;

public sealed class VoteUpdationRequest : BaseDto
{
    public Guid RatingId { get; init; }

    public Guid UserId { get; init; }

    public VoteUpdationRequest(Guid id, Guid ratingId, Guid userId) : base(id)
    {
        RatingId = ratingId;
        UserId = userId;
    }
}