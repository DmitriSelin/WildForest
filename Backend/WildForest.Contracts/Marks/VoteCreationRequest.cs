namespace WildForest.Contracts.Marks;

public sealed record VoteCreationRequest(Guid MarkId, Guid UserId);