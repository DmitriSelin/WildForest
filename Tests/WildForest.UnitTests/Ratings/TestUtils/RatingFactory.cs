using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Enums;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.UnitTests.Ratings.TestUtils;

public static class RatingFactory
{
    /// <summary>
    /// Factory for creating test weather rating
    /// </summary>
    /// <param name="weatherForecastId">Current weather id</param>
    /// <param name="firstUserId">User's id with like vote</param>
    /// <param name="secondUserId">User's id with dislike vote</param>
    /// <returns>Test rating with 2 votes</returns>
    public static Rating Create(WeatherForecastId weatherForecastId, UserId firstUserId, UserId secondUserId)
    {
        var rating = Rating.Create(weatherForecastId);
        rating.CreateVote(firstUserId, VoteResult.Up);
        rating.CreateVote(secondUserId, VoteResult.Down);

        return rating;
    }
}