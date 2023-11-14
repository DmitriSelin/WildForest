namespace WildForest.Application.Comments.Commands.Services;

public sealed record CommentCommand(Guid UserId, Guid WeatherForecastId, string Text);