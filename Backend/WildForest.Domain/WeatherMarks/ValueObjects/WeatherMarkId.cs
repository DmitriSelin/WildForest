using WildForest.Domain.Common.Models;

namespace WildForest.Domain.WeatherMarks.ValueObjects;

public sealed class WeatherMarkId : ValueObject
{
    public Guid Value { get; }

    private WeatherMarkId(Guid value)
        => Value = value;

    public static WeatherMarkId Create()
    {
        return new(Guid.NewGuid());
    }

    public static WeatherMarkId Create(Guid value)
    {
        return new(value);
    }

    public static WeatherMarkId Parse(string weatherMarkId)
    {
        return Create(Guid.Parse(weatherMarkId));
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