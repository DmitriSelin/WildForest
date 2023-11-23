using System.Security.Cryptography;
namespace WildForest.Application.Comments.Commands.Services;

public sealed record CommentCommandForUpdate(Guid Id, Guid WeatherForecastId, Guid UserId, string NewText);