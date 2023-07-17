using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects;

public sealed class WeatherForecastId : ValueObject
{
    public Guid Value { get; }

    private WeatherForecastId(Guid value)
        => Value = value;

    public static WeatherForecastId Create()
        => new(Guid.NewGuid());

    public static WeatherForecastId Create(Guid value)
        => new(value);

    public override string ToString()
        => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}