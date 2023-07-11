using System.Text.Json.Serialization;
using System.Text.Json;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.Weather.Entities;

namespace WildForest.Infrastructure.Http.JsonConverters;

public sealed class WeatherForecastConverter : JsonConverter<List<ThreeHourWeatherForecast>>
{
    private readonly CityId _cityId;

    public WeatherForecastConverter(CityId cityId)
        => _cityId = cityId;

    public override List<ThreeHourWeatherForecast>? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        DateTime date = DateTime.MinValue;
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

        List<ThreeHourWeatherForecast>? weatherForecasts = new();

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
                        date = GetDateTime(reader.GetString());
                        isFilled = true;
                        break;
                }

                if (isFilled)
                {
                    var forecast = CreateThreeHourWeatherForecast(
                        date,
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

    private static DateTime GetDateTime(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return DateTime.Parse(value);
        }

        throw new ArgumentNullException(nameof(value));
    }

    private ThreeHourWeatherForecast CreateThreeHourWeatherForecast(
        DateTime date,
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
        var temperature = Temperature.Create(temperatureValue, temperatureFeelsLike);
        var pressure = Pressure.Create(pressureValue);
        var humidity = Humidity.Create(humidityValue);
        var weather = WeatherDescription.Create(weatherName, weatherDescription);
        var cloudiness = Cloudiness.Create(cloudinessValue);
        var wind = Wind.Create(windSpeed, windDirection, windGust);
        var visibility = Visibility.Create(visibilityValue);
        var precipitationProbability = PrecipitationProbability.Create(precipitationProbabilityValue);

        PrecipitationVolume? precipitationVolume;

        if (precipitationVolumeValue is not null)
        {
            precipitationVolume = PrecipitationVolume.Create((double)precipitationVolumeValue);
        }
        else
        {
            precipitationVolume = null;
        }

        var forecast = ThreeHourWeatherForecast.Create(
            date,
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

    public override void Write(Utf8JsonWriter writer, List<ThreeHourWeatherForecast> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}