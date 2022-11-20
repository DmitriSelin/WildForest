using WildForest.Domain.City.ValueObjects;
using WildForest.Domain.User.ValueObjects;

namespace WildForest.Application.Weather.Queries.DayWeather
{
    public record DayWeatherQuery(UserId UserId, CityId CityId);
}