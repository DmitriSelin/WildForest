
namespace WildForest.Contracts.Comments;

public sealed record CommentRequestForUpdate(Guid Id, Guid WeatherForecastId, string NewText) : CommentRequest(WeatherForecastId, NewText);