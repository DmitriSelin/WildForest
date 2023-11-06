namespace WildForest.Application.Ratings.Common;

public sealed record RatingDto(Guid RatingId, Guid VoteId, byte VoteResult, int Points);