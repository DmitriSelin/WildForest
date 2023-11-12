using WildForest.Domain.Comments.ValueObjects;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Comments.Entities;

public sealed class Comment : Entity<CommentId>
{
    public string Text { get; private set; } = null!;

    public DateTime Date { get; }

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public WeatherForecastId WeatherForecastId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    public static Comment Create(string value, UserId userId, WeatherForecastId weatherForecastId)
    {
        string text = ValidateText(value);

        return new(
            CommentId.Create(), text, DateTime.UtcNow,
            userId, weatherForecastId);
    }

    private static string ValidateText(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        string text = value.Trim();

        if (text.Length is < 1 or > 200)
            throw new ArgumentException("Invalid comment's length", nameof(value));

        return text;
    }

    private Comment(CommentId id, string text, DateTime date, UserId userId, WeatherForecastId weatherForecastId) : base(id)
    {
        Text = text;
        Date = date;
        UserId = userId;
        WeatherForecastId = weatherForecastId;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private Comment(CommentId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}