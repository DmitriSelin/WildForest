using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Application.Common.Converters
{
    public class WeatherForecastConverter : JsonConverter<List<WeatherForecast>>
    {
        public override List<WeatherForecast>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new();
        }

        public override void Write(Utf8JsonWriter writer, List<WeatherForecast> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}