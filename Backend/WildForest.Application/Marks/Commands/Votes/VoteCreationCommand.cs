namespace WildForest.Application.Marks.Commands.Votes;

public sealed record VoteCreationCommand
{
    public Guid MarkId { get; }

    public Guid UserId { get; }

    public byte VoteResult { get; }

    public static VoteCreationCommand CreateUpVote(Guid markId, Guid userId)
        => new(markId, userId, 1);

    public static VoteCreationCommand CreateDownVote(Guid markId, Guid userId)
        => new(markId, userId, 2);

    private VoteCreationCommand(Guid markId, Guid userId, byte voteResult)
    {
        MarkId = markId;
        UserId = userId;
        VoteResult = voteResult;
    }
}