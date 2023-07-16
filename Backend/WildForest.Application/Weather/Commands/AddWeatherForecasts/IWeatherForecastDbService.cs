namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public interface IWeatherForecastDbService
{
    Task AddWeatherForecastsInDbAsync();
}