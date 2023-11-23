namespace WildForest.Contracts.Comments;

public record CommentRequest(Guid WeatherForecastId, string Text);