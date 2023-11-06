namespace WildForest.Contracts.Ratings;

public sealed record VoteUpdationRequest(Guid VoteId, Guid RatingId, Guid UserId);