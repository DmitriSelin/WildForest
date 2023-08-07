using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.Entities;
using WildForest.Domain.Marks.Enums;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Marks;

public sealed class Mark : AggregateRoot<MarkId>
{
    public int Points { get; private set; }

    public WeatherForecastId WeatherForecastId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    private readonly List<Vote> votes = new();

    public IEnumerable<Vote> Votes => votes.ToList();

    public Vote ChangeRating(UserId userId, VoteResult result)
    {
        Vote vote;

        if (result == VoteResult.Up)
        {
            vote = Vote.Create(userId, VoteResult.Up, Id);
            Points++;
        }
        else
        {
            vote = Vote.Create(userId, VoteResult.Down, Id);
            Points--;
        }

        votes.Add(vote);
        return vote;
    }

    public static Mark Create(WeatherForecastId weatherForecastId)
        => new(MarkId.Create(), weatherForecastId);

    private Mark(MarkId id, WeatherForecastId weatherForecastId) : base(id)
    {
        Points = 0;
        WeatherForecastId = weatherForecastId;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private Mark(MarkId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}