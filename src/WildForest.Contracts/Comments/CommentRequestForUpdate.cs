namespace WildForest.Contracts.Comments;

public sealed class CommentRequestForUpdate : CommentRequest
{
    public Guid Id { get; init; }

    public CommentRequestForUpdate(Guid id, Guid weatherForecastId, string text) : base(weatherForecastId, text)
    {
        Id = id;
    }
}