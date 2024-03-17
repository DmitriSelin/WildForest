using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;

public interface IWeatherForecastFactory
{
    IEnumerable<WeatherForecast> Create(
        List<WeatherForecastVm> forecasts, CityId cityId);
}