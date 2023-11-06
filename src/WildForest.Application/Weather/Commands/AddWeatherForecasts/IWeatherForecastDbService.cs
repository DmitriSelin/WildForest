using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts;

public interface IWeatherForecastDbService
{
    Task AddWeatherForecastsInDbAsync(CityId cityId);
}