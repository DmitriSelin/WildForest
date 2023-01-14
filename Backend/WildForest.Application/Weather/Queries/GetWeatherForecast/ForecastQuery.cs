namespace WildForest.Application.Weather.Queries.GetWeatherForecast
{
    public sealed record ForecastQuery(Guid UserId, Guid CityId, DateTime WeatherDate);
}