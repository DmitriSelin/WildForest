namespace WildForest.Application.Weather.Common.Models;

public sealed record WeatherForecastDto(
    Guid WeatherId,
    DateTime Date,
    Temperature Temperature,
    int Pressure,
    byte Humidity,
    WeatherDescription Description,
    byte Cloudiness,
    Wind Wind,
    double Visibility,
    byte PrecipitationProbability,
    double PrecipitationVolume);