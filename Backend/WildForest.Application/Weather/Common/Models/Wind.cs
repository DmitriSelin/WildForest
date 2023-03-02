namespace WildForest.Application.Weather.Common.Models;

public sealed record Wind(
    double Speed, 
    int Direction, 
    double Gust);