namespace WildForest.Contracts.Comments;

public sealed class CommentRequestForUpdate : CommentRequest
{
    public Guid Id { get; init; }

    public CommentRequestForUpdate(Guid id, Guid userId, Guid weatherForecastId, string text) : base(userId, weatherForecastId, text)
    {
        Id = id;
    }
}