namespace WildForest.Application.Ratings.Commands.Votes;

public sealed record VoteCreationCommand
{
    public Guid RatingId { get; }

    public Guid UserId { get; }

    public byte VoteResult { get; }

    public VoteCreationCommand(Guid ratingId, Guid userId, byte voteResult)
    {
        RatingId = ratingId;
        UserId = userId;
        VoteResult = voteResult;
    }
}