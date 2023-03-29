using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects;

public sealed class Rating : ValueObject
{
    public byte Value { get; }

    private Rating(byte value)
        => Value = value;

    public static Rating Create(byte value)
    {
        if (value < 1 || value > 5)
            throw new ArgumentException(nameof(value));

        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}