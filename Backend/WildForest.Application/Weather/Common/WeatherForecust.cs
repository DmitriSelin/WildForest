namespace WildForest.Application.Weather.Common
{
    public sealed record WeatherForecust(
        Guid WeatherId,
        string DaySpan,
        string Temperature,
        string Description,
        string Humidity,
        string Pressure,
        string Wind,
        string Cloudness,
        string Storm);   //TODO: Add another properties: user marks, comments
}