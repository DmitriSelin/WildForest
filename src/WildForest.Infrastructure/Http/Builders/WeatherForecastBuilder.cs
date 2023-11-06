using System.Text.Json;
using WildForest.Application.Weather.Common.JsonModels;

namespace WildForest.Infrastructure.Http.Builders;

public sealed class WeatherForecastBuilder : IWeatherForecastBuilder
{
    private WeatherForecastVm weatherForecastVm = new();

    public void BuildProperty(string propertyName, ref Utf8JsonReader reader)
    {
        SelectProperty(propertyName, ref reader);
    }

    public WeatherForecastVm Build()
    {
        return new(weatherForecastVm);
    }

    private void SelectProperty(string propertyName, ref Utf8JsonReader reader)
    {
        switch (propertyName)
        {
            case "temp":
                weatherForecastVm.Temperature = reader.GetDouble();
                break;
            case "feels_like":
                weatherForecastVm.TemperatureFeelsLike = reader.GetDouble();
                break;
            case "pressure":
                weatherForecastVm.Pressure = ConvertPressure(reader.GetDouble());
                break;
            case "humidity":
                weatherForecastVm.Humidity = reader.GetByte();
                break;
            case "main":
                if (reader.TokenType == JsonTokenType.StartObject)
                    break;
                weatherForecastVm.Name = reader.GetString()!;
                break;
            case "description":
                weatherForecastVm.Description = reader.GetString()!;
                break;
            case "all":
                weatherForecastVm.Cloudiness = reader.GetByte();
                break;
            case "speed":
                weatherForecastVm.WindSpeed = reader.GetDouble();
                break;
            case "deg":
                weatherForecastVm.WindDirection = reader.GetInt32();
                break;
            case "gust":
                weatherForecastVm.WindGust = reader.GetDouble();
                break;
            case "visibility":
                weatherForecastVm.Visibility = reader.GetDouble();
                break;
            case "pop":
                weatherForecastVm.PrecipitationProbability = ConvertPrecipitationProbabilityToPercents(reader.GetDouble());
                break;
            case "3h":
                weatherForecastVm.PrecipitationVolume = reader.GetDouble();
                break;
            case "dt_txt":
                weatherForecastVm.Date = GetDateTimeFromString(reader.GetString());
                break;
        }
    }

    /// <summary>
    /// Convert pressure from hPa to mmHg
    /// </summary>
    /// <param name="value">Pressure in hPa</param>
    /// <returns>Pressure in mmHg</returns>
    private static int ConvertPressure(double value)
    {
        double divider = 1.333;
        int pressure = Convert.ToInt32(value / divider);

        return pressure;
    }

    private static byte ConvertPrecipitationProbabilityToPercents(double value)
    {
        value *= 100;

        byte result = Convert.ToByte(value);
        return result;
    }

    private static DateTime GetDateTimeFromString(string? value)
        => DateTime.Parse(value!);
}