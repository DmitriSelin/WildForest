namespace WildForest.Application.Weather.Common.JsonModels;

public sealed class WeatherForecastVm
{
    public DateTime Date { get; set; }

    public double Temperature { get; set; }

    public double TemperatureFeelsLike { get; set; }

    public int Pressure { get; set; }

    public byte Humidity { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte Cloudiness { get; set; }

    public double WindSpeed { get; set; }

    public int WindDirection { get; set; }

    public double WindGust { get; set; }

    public double Visibility { get; set; }

    public byte PrecipitationProbability { get; set; }

    public double? PrecipitationVolume { get; set; }

    public WeatherForecastVm() { }

    public WeatherForecastVm(WeatherForecastVm vm)
    {
        Date = vm.Date;
        Temperature = vm.Temperature;
        TemperatureFeelsLike = vm.TemperatureFeelsLike;
        Pressure = vm.Pressure;
        Humidity = vm.Humidity;
        Name = vm.Name;
        Description = vm.Description;
        Cloudiness = vm.Cloudiness;
        WindSpeed = vm.WindSpeed;
        WindDirection = vm.WindDirection;
        WindGust = vm.WindGust;
        Visibility = vm.Visibility;
        PrecipitationProbability = vm.PrecipitationProbability;

        if (vm.PrecipitationVolume != null)
            PrecipitationVolume = vm.PrecipitationVolume;
    }
}