using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Api.Common.JsonConverters
{
    public sealed class WeatherForecastConverter : JsonConverter<List<WeatherForecast>>
    {
        private readonly CityId _cityId;

        public WeatherForecastConverter(CityId cityId)
            => _cityId = cityId;

        public override List<WeatherForecast>? Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            DateOnly date = DateOnly.MinValue;
            TimeOnly time = TimeOnly.MinValue;
            double temperatureValue = 0;
            double temperatureFeelsLike = 0;
            int pressureValue = 0;
            byte humidityValue = 0;
            string weatherName = string.Empty;
            string weatherDescription = string.Empty;
            byte cloudinessValue = 0;
            double windSpeed = 0;
            int windDirection = 0;
            double windGust = 0;
            double visibilityValue = 0;
            byte precipitationProbabilityValue = 0;
            double? precipitationVolume = null;

            bool isFilled = false;

            List<WeatherForecast>? weatherForecasts = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "temp":
                            temperatureValue = reader.GetDouble();
                            break;
                        case "feels_like":
                            temperatureFeelsLike = reader.GetDouble();
                            break;
                        case "pressure":
                            pressureValue = GetPressure(reader.GetDouble());
                            break;
                        case "humidity":
                            humidityValue = reader.GetByte();
                            break;
                        case "main":
                            if (reader.TokenType == JsonTokenType.StartObject)
                                break;
                            weatherName = GetStringValue(reader.GetString());
                            break;
                        case "description":
                            weatherDescription = GetStringValue(reader.GetString());
                            break;
                        case "all":
                            cloudinessValue = reader.GetByte();
                            break;
                        case "speed":
                            windSpeed = reader.GetDouble();
                            break;
                        case "deg":
                            windDirection = reader.GetInt32();
                            break;
                        case "gust":
                            windGust = reader.GetDouble();
                            break;
                        case "visibility":
                            visibilityValue = reader.GetDouble();
                            break;
                        case "pop":
                            precipitationProbabilityValue = GetPrecipitationProbability(reader.GetDouble());
                            break;
                        case "3h":
                            precipitationVolume = reader.GetDouble();
                            break;
                        case "dt_txt":
                            var dateAndTime = GetDateAndTime(reader.GetString());
                            date = dateAndTime.Item1;
                            time = dateAndTime.Item2;
                            isFilled = true;
                            break;
                    }

                    if (isFilled)
                    {
                        var forecast = CreateWeatherForecast(
                            date,
                            time,
                            temperatureValue,
                            temperatureFeelsLike,
                            pressureValue,
                            humidityValue,
                            weatherName,
                            weatherDescription,
                            cloudinessValue,
                            windSpeed,
                            windDirection,
                            windGust,
                            visibilityValue,
                            precipitationProbabilityValue,
                            precipitationVolume);

                        weatherForecasts.Add(forecast);

                        isFilled = false;
                    }
                }
            }

            return weatherForecasts;
        }

        private static string GetStringValue(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                return value;

            throw new ArgumentNullException(nameof(value));
        }

        private static int GetPressure(double value)
        {
            double divider = 1.333;     //transfer from hPa to mmHg
            int pressure = Convert.ToInt32(value / divider);

            return pressure;
        }

        private static byte GetPrecipitationProbability(double value)
        {
            value *= 100;

            byte result = Convert.ToByte(value);
            return result;
        }

        private static (DateOnly, TimeOnly) GetDateAndTime(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                string[] array = value.Split(' ');

                if (DateOnly.TryParse(array[0], out DateOnly date))
                {
                    if (TimeOnly.TryParse(array[1], out TimeOnly time))
                    {
                        return new(date, time);
                    }
                }
            }

            throw new ArgumentException(nameof(value));
        }

        private WeatherForecast CreateWeatherForecast(
            DateOnly date,
            TimeOnly time,
            double temperatureValue,
            double temperatureFeelsLike,
            int pressureValue,
            byte humidityValue,
            string weatherName,
            string weatherDescription,
            byte cloudinessValue,
            double windSpeed,
            int windDirection,
            double windGust,
            double visibilityValue,
            byte precipitationProbabilityValue,
            double? precipitationVolumeValue)
        {
            var forecastDate = ForecastDate.Create(date);
            var forecastTime = ForecastTime.Create(time);
            var temperature = Temperature.Create(temperatureValue, temperatureFeelsLike);
            var pressure = Pressure.Create(pressureValue);
            var humidity = Humidity.Create(humidityValue);
            var weather = WeatherDescription.Create(weatherName, weatherDescription);
            var cloudiness = Cloudiness.Create(cloudinessValue);
            var wind = Wind.Create(windSpeed, windDirection, windGust);
            var visibility = Visibility.Create(visibilityValue);
            var precipitationProbability = PrecipitationProbability.Create(precipitationProbabilityValue);
            var precipitationVolume = PrecipitationVolume.Create(precipitationVolumeValue);

            var forecast = WeatherForecast.Create(
                forecastDate,
                forecastTime,
                temperature,
                pressure,
                humidity,
                weather,
                cloudiness,
                wind,
                visibility,
                precipitationProbability,
                precipitationVolume,
                _cityId);

            return forecast;
        }

        public override void Write(Utf8JsonWriter writer, List<WeatherForecast> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}