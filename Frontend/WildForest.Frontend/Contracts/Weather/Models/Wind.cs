namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed record Wind(
    double Speed, 
    int Direction, 
    double Gust);