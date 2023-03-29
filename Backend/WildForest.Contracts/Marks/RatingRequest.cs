namespace WildForest.Contracts.Marks;

public sealed record RatingRequest(Guid WeatherId, Guid UserId, byte Rating);