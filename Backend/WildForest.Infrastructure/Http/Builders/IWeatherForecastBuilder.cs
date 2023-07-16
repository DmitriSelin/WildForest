using System.Text.Json;
using WildForest.Application.Weather.Common.JsonModels;

namespace WildForest.Infrastructure.Http.Builders;

public interface IWeatherForecastBuilder
{
    void BuildProperty(string propertyName, ref Utf8JsonReader reader);

    WeatherForecastVm Build();
}