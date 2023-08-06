using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Enums;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Marks.Entities;

public sealed class Vote : Entity<VoteId>
{
    public VoteResult Result { get; private set; }

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public MarkId MarkId { get; } = null!;

    public Mark Mark { get; } = null!;

    public void Up()
    {
        Result = VoteResult.Up;
    }

    public void Down()
    {
        Result = VoteResult.Down;
    }

    internal static Vote Create(UserId userId, VoteResult result, MarkId markId)
        => new(VoteId.Create(), result, userId, markId);

    private Vote(
        VoteId id, VoteResult result,
        UserId userId, MarkId markId) : base(id)
    {
        Result = result;
        UserId = userId;
        MarkId = markId;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private Vote(VoteId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}