using WildForest.Domain.Common.Models;
using WildForest.Domain.Votes.ValueObjects;

namespace WildForest.Domain.Votes.Entities;

public sealed class Vote : Entity<VoteId>
{
    public int Points { get; private set; }

    public static Vote Create(VoteId voteId)
        => new Vote(voteId);

    public void Up()
        => Points++;

    public void Down()
        => Points--;

    private Vote(VoteId id) : base(id)
    {
        Points = 0;
    }
}