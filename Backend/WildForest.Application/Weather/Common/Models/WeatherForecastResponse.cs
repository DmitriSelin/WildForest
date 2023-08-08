namespace WildForest.Application.Weather.Common.Models;

public sealed record WeatherForecastResponse(
    Guid WeatherForecastId,
    DateOnly Date,
    List<WeatherForecastDto> WeatherForecasts,
    Guid RatingId,
    int Points);