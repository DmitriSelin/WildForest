using WildForest.Dto.Models;

namespace WildForest.Application.Comments.Commands.Services;

public sealed class CommentCommandForUpdate : BaseDto
{
    public Guid WeatherForecastId { get; init; }

    public Guid UserId { get; init; }

    public string NewText { get; init; }

    public CommentCommandForUpdate(Guid id, Guid weatherForecastId, Guid userId, string newText) : base(id)
    {
        WeatherForecastId = weatherForecastId;
        UserId = userId;
        NewText = newText;
    }
}