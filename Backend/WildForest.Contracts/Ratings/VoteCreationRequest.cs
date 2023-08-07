namespace WildForest.Contracts.Ratings;

public sealed record VoteCreationRequest(Guid RatingId, Guid UserId);