using System.Text.Json.Serialization;
using System.Text.Json;
using WildForest.Infrastructure.Http.Builders;
using WildForest.Application.Weather.Common.JsonModels;

namespace WildForest.Infrastructure.Http.JsonConverters;

public sealed class WeatherForecastConverter : JsonConverter<List<WeatherForecastVm>>
{
    private const string dateTimePropertyName = "dt_txt";
    private readonly IWeatherForecastBuilder _weatherForecastBuilder;

    public WeatherForecastConverter(IWeatherForecastBuilder weatherForecastBuilder)
    {
        _weatherForecastBuilder = weatherForecastBuilder;
    }

    public override List<WeatherForecastVm>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        List<WeatherForecastVm> weatherForecasts = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString()!;

                ReadJsonProperty(ref reader, propertyName);

                if (propertyName == dateTimePropertyName)
                {
                    var weatherForecast = _weatherForecastBuilder.Build();
                    weatherForecasts.Add(weatherForecast);
                }
            }
        }

        return weatherForecasts;
    }

    private void ReadJsonProperty(ref Utf8JsonReader reader, string propertyName)
    {
        reader.Read();
        _weatherForecastBuilder.BuildProperty(propertyName, ref reader);
    }

    public override void Write(Utf8JsonWriter writer, List<WeatherForecastVm> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}