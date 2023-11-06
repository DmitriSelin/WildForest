namespace WildForest.Application.Weather.Common.Models.Properties;

public sealed record Wind(
    double Speed,
    int Direction,
    double Gust);