using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects;

public sealed class ThreeHourWeatherForecastId : ValueObject
{
    public Guid Value { get; }

    private ThreeHourWeatherForecastId(Guid value)
        => Value = value;

    public static ThreeHourWeatherForecastId Create()
        => new(Guid.NewGuid());

    public static ThreeHourWeatherForecastId Create(Guid value)
        => new(value);

    public override string ToString()
        => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}