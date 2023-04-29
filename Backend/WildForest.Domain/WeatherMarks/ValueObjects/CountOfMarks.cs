using WildForest.Domain.Common.Models;

namespace WildForest.Domain.WeatherMarks.ValueObjects;

public sealed class CountOfMarks : ValueObject
{
    public uint Value { get; private set; }

    private CountOfMarks(uint value)
        => Value = value;

    public static CountOfMarks Create(uint value = 0)
    {
        return new(value);
    }

    internal void Increment()
    {
        Value++;
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