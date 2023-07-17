using WildForest.Domain.Common.Models;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Votes.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Votes.Entities;

public sealed class Vote : Entity<VoteId>
{
    public int Points { get; private set; }

    public UserId UserId { get; } = null!;

    public User User { get; } = null!;

    public WeatherForecastId WeatherForecastId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    public static Vote Create(UserId userId, WeatherForecastId dayWeatherForecastId)
        => new(VoteId.Create(), userId, dayWeatherForecastId);

    public void Up()
        => Points++;

    public void Down()
        => Points--;

    private Vote(VoteId id, UserId userId, WeatherForecastId weatherForecastId) : base(id)
    {
        Points = 0;
        WeatherForecastId = weatherForecastId;
        UserId = userId;
    }

    private Vote(VoteId id) : base(id) { }
}