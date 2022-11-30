using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Weather.Queries.DayWeather
{
    public record DayWeatherQuery(UserId UserId, CityId CityId);
}