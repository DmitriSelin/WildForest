namespace WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

public sealed record HomeWeatherForecastQuery(Guid UserId, DateOnly ForecastDate);