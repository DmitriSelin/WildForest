namespace WildForest.Application.Weather.Queries.GetTodayForecast
{
    public sealed record ForecastQuery(Guid UserId, Guid CityId);
}