using WildForest.Domain.Common.Models;
using WildForest.Domain.Ratings.Enums;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Domain.Ratings.Entities;

public sealed class Vote : Entity<VoteId>
{
    public VoteResult Result { get; private set; }

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public RatingId RatingId { get; } = null!;

    public Rating Rating { get; } = null!;

    public void Up()
    {
        Result = VoteResult.Up;
    }

    public void Down()
    {
        Result = VoteResult.Down;
    }

    internal static Vote Create(UserId userId, VoteResult result, RatingId ratingId)
        => new(VoteId.Create(), result, userId, ratingId);

    private Vote(
        VoteId id, VoteResult result,
        UserId userId, RatingId ratingId) : base(id)
    {
        Result = result;
        UserId = userId;
        RatingId = ratingId;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private Vote(VoteId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}