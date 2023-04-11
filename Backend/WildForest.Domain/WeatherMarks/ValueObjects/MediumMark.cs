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

    /// <summary>
    /// Use for creating new entry
    /// </summary>
    /// <returns>Medium mark == 0</returns>
    public static MediumMark CreateNewProperty()
        => new(0);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}