namespace WildForest.Contracts.Comments;

public sealed record CommentRequest(Guid WeatherForecastId, string Text);