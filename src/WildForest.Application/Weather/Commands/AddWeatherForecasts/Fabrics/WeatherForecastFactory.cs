using WildForest.Application.Weather.Common.JsonModels;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Application.Weather.Commands.AddWeatherForecasts.Fabrics;

public sealed class WeatherForecastFactory : IWeatherForecastFactory
{
    public IEnumerable<WeatherForecast> Create(
        List<WeatherForecastVm> forecasts, CityId cityId)
    {
        List<WeatherForecast> weatherForecasts = new();

        var currentDate = forecasts[0].Date.Date;
        var weatherForecast = CreateWeatherForecast(forecasts[0], cityId);
        weatherForecasts.Add(weatherForecast);

        foreach (var forecast in forecasts)
        {
            if (forecast.Date.Date > currentDate)
            {
                weatherForecast = CreateWeatherForecast(forecast, cityId);
                weatherForecasts.Add(weatherForecast);
                currentDate = forecast.Date.Date;
            }

            var threeHourWeatherForecast = CreateThreeHourWeatherForecast(forecast, weatherForecast.Id);
            weatherForecast.AddThreeHourWeatherForecast(threeHourWeatherForecast);
        }

        return weatherForecasts;
    }

    private WeatherForecast CreateWeatherForecast(WeatherForecastVm forecast, CityId cityId)
    {
        var date = DateOnly.FromDateTime(forecast.Date);
        return WeatherForecast.Create(date, cityId);
    }

    private ThreeHourWeatherForecast CreateThreeHourWeatherForecast(
        WeatherForecastVm forecast, WeatherForecastId weatherForecastId)
    {
        var time = TimeOnly.FromDateTime(forecast.Date);
        var temperature = Temperature.Create(forecast.Temperature, forecast.TemperatureFeelsLike);
        var pressure = Pressure.Create(forecast.Pressure);
        var humidity = Humidity.Create(forecast.Humidity);
        var description = WeatherDescription.Create(forecast.Name, forecast.Description);
        var cloudiness = Cloudiness.Create(forecast.Cloudiness);
        var wind = Wind.Create(forecast.WindSpeed, forecast.WindDirection, forecast.WindGust);
        var visibility = Visibility.Create(forecast.Visibility);
        var precipitationProbability = PrecipitationProbability.Create(forecast.PrecipitationProbability);
        PrecipitationVolume? precipitationVolume = null!;

        if (forecast.PrecipitationVolume != null)
            precipitationVolume = PrecipitationVolume.Create((double)forecast.PrecipitationVolume);

        return ThreeHourWeatherForecast.Create(
            time, temperature, pressure,
            humidity, description, cloudiness,
            wind, visibility, precipitationProbability,
            precipitationVolume, weatherForecastId);
    }
}