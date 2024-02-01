using WildForest.Dto.Models;

namespace WildForest.Application.Ratings.Common;

public sealed class VoteDto : BaseDto
{
    public byte VoteResult { get; init; }

    public VoteDto(Guid id, byte voteResult) : base(id)
    {
        VoteResult = voteResult;
    }
}