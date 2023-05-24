using System;

namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed record WeatherForecastDto(
    Guid WeatherId,
    DateOnly Date,
    TimeOnly Time,
    Temperature Temperature,
    int Pressure,
    byte Humidity,
    WeatherDescription WeatherDescription,
    byte Cloudiness,
    Wind Wind,
    double Visibility,
    byte PrecipitationProbability,
    double PrecipitationVolume);