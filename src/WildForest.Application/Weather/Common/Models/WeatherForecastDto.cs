using WildForest.Application.Weather.Common.Models.Properties;

namespace WildForest.Application.Weather.Common.Models;

public sealed record WeatherForecastDto(
    TimeOnly Time,
    Temperature Temperature,
    int Pressure,
    byte Humidity,
    WeatherDescription Description,
    byte Cloudiness,
    Wind Wind,
    double Visibility,
    byte PrecipitationProbability,
    double PrecipitationVolume);