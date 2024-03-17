using WildForest.Dto.Models;

namespace WildForest.Application.Ratings.Common;

public sealed class RatingDto : BaseDto
{
    public Guid? VoteId { get; init; }

    public byte VoteResult { get; init; }

    public int Points { get; init; }

    public RatingDto(Guid id, Guid? voteId, byte voteResult, int points) : base(id)
    {
        VoteId = voteId;
        VoteResult = voteResult;
        Points = points;
    }
}