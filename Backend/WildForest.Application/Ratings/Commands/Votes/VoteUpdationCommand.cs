namespace WildForest.Application.Ratings.Commands.Votes;

public sealed record VoteUpdationCommand(Guid VoteId, Guid RatingId, Guid UserId);