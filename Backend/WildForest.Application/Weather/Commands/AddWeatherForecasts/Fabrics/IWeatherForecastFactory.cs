using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;

public interface IWeatherForecastFactory
{
    Tuple<IEnumerable<WeatherForecast>, IEnumerable<ThreeHourWeatherForecast>> Create(
        List<WeatherForecastVm> forecasts, CityId cityId);
}