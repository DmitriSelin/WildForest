namespace WildForest.Contracts.Comments;

public class CommentRequest
{
    public Guid UserId { get; init; }

    public Guid WeatherForecastId { get; init; }

    public string Text { get; init; }

    public CommentRequest(Guid userId, Guid weatherForecastId, string text)
    {
        UserId = userId;
        WeatherForecastId = weatherForecastId;
        Text = text;
    }
}