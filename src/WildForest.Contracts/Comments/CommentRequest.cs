namespace WildForest.Contracts.Comments;

public class CommentRequest
{
    public Guid WeatherForecastId { get; init; }

    public string Text { get; init; }

    public CommentRequest(Guid weatherForecastId, string text)
    {
        WeatherForecastId = weatherForecastId;
        Text = text;
    }
}