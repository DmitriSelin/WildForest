using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Api.Common.Converters
{
    public class WeatherForecastConverter : JsonConverter<List<WeatherForecast>>
    {
        public override List<WeatherForecast>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateOnly date;
            TimeOnly time;
            double temperatureValue;
            double temperatureFeelsLike;
            double pressureValue;
            byte humidityValue;
            string weatherName;
            string weatherDescription;
            double cloudinessValue;
            double windSpeed;
            int windDirection;
            double windGust;
            double visibilityValue;
            byte precipitationProbabilityValue;
            double precipitationVolume;

            List<WeatherForecast>? weatherForecasts = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    string? property;
                    string value;
                    reader.Read();

                    switch (propertyName)
                    {
                        case "temp":
                            value = GetNotNullValue(ref reader, property);
                            break;
                        case "feels_like":
                            value = GetNotNullValue(ref reader, property);
                            temperatureFeelsLike = GetDoubleValue(value);
                            break;
                        case "pressure":

                            break;
                        case "humidity":
                            break;
                        case "main":
                            break;
                        case "description":
                            break;
                        case "all":
                            break;
                        case "speed":
                            break;
                        case "deg":
                            break;
                        case "gust":
                            break;
                        case "visibility":
                            break;
                        case "pop":
                            break;
                        case "3h":
                            break;
                        case "dt_txt":
                            break;
                    }
                }
            }

            return weatherForecasts;
        }

        private static string GetNotNullValue(ref Utf8JsonReader reader, string? property)
        {
            property = reader.GetString();

            if (property is not null && property != string.Empty)
            {
                return property;
            }

            throw new ArgumentNullException(nameof(property));
        }

        private static double GetDoubleValue(string value)
        {
            double result;

            if (double.TryParse(value, out result))
            {
                return result;
            }

            throw new ArgumentException(nameof(value));
        }

        public override void Write(Utf8JsonWriter writer, List<WeatherForecast> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}