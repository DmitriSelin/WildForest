using WildForest.Domain.Common.Models;
using WildForest.Domain.Ratings.Entities;
using WildForest.Domain.Ratings.Enums;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Ratings;

public sealed class Rating : AggregateRoot<RatingId>
{
    public int Points { get; private set; }

    public WeatherForecastId WeatherForecastId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    private readonly List<Vote> votes = new();

    public IEnumerable<Vote> Votes => votes.ToList();

    public Vote CreateVote(UserId userId, VoteResult result)
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

    public void ChangeVote(Vote vote)
    {
        if (vote.Result == VoteResult.Up)
        {
            Points--;
            vote.Down();
        }
        else
        {
            Points++;
            vote.Up();
        }
    }

    public static Rating Create(WeatherForecastId weatherForecastId)
        => new(RatingId.Create(), weatherForecastId);

    private Rating(RatingId id, WeatherForecastId weatherForecastId) : base(id)
    {
        Points = 0;
        WeatherForecastId = weatherForecastId;
    }

#pragma warning disable IDE0051 // Remove unused private members
    private Rating(RatingId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}