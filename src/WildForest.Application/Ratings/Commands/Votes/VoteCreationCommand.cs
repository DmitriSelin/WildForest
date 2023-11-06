namespace WildForest.Application.Ratings.Commands.Votes;

public sealed record VoteCreationCommand
{
    public Guid RatingId { get; }

    public Guid UserId { get; }

    public byte VoteResult { get; }

    public static VoteCreationCommand CreateUpVote(Guid ratingId, Guid userId)
        => new(ratingId, userId, 1);

    public static VoteCreationCommand CreateDownVote(Guid ratingId, Guid userId)
        => new(ratingId, userId, 2);

    private VoteCreationCommand(Guid ratingId, Guid userId, byte voteResult)
    {
        RatingId = ratingId;
        UserId = userId;
        VoteResult = voteResult;
    }
}