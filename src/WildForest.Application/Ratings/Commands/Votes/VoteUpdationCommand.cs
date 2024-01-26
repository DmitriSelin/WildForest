using WildForest.Dto.Models;

namespace WildForest.Application.Ratings.Commands.Votes;

public sealed class VoteUpdationCommand : BaseDto
{
    public Guid RatingId { get; init; }

    public Guid UserId { get; init; }

    public VoteUpdationCommand(Guid id, Guid ratingId, Guid userId) : base(id)
    {
        RatingId = ratingId;
        UserId = userId;
    }
}