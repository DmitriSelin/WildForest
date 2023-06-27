using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects;

public sealed class WeatherForecastId : ValueObject
{
    public Guid Value { get; }

    private WeatherForecastId(Guid value)
    {
        Value = value;
    }

    public static WeatherForecastId Create()
    {
        return new(Guid.NewGuid());
    }

    public static WeatherForecastId Create(Guid value)
    {
        return new(value);
    }

    public static WeatherForecastId Parse(string weatherId)
    {
        return Create(Guid.Parse(weatherId));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}