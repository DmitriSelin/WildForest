namespace WildForest.Contracts.Marks;

public sealed record CommentRequest(Guid WeatherId, Guid UserId, byte StarsCount, string Comment);