using WildForest.Domain.Common.Models;

namespace WildForest.Domain.WeatherMarks.ValueObjects;

public sealed class MediumMark : ValueObject
{
    public double Value { get; }

    private MediumMark(double value)
        => Value = value;

    public static MediumMark Create(double value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentException("Not correct mark", nameof(value));

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}