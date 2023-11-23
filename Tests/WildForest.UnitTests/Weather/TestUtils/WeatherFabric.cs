using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Weather;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.UnitTests.Weather.TestUtils;

public static class WeatherFabric
{
    public static WeatherForecast Create(CityId cityId)
    {
        var weather = WeatherForecast.Create(DateOnly.FromDateTime(DateTime.Now), cityId);
        var threeHourWeatherForecasts = CreateThreeHourWeatherForecasts(weather.Id);
        weather.AddThreeHourWeatherForecast(threeHourWeatherForecasts);

        return weather;
    }

    private static ThreeHourWeatherForecast CreateThreeHourWeatherForecasts(WeatherForecastId weatherForecastId)
    {
        var time = TimeOnly.FromDateTime(DateTime.Now);
        var temperature = Temperature.Create(20, 19);
        var pressure = Pressure.Create(600);
        var humidity = Humidity.Create(90);
        var description = WeatherDescription.Create("Test name", "Test description");
        var cloudiness = Cloudiness.Create(50);
        var wind = Wind.Create(20, 200, 23);
        var visibility = Visibility.Create(10_000);
        var precipitationProbability = PrecipitationProbability.Create(50);
        var precipitationVolume = PrecipitationVolume.Create(5);

        return ThreeHourWeatherForecast.Create(
            time, temperature, pressure, humidity, description, cloudiness,
            wind, visibility, precipitationProbability, precipitationVolume, weatherForecastId);
    }
}