using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects;

public sealed class Date : ValueObject
{
    public DateTime Value { get; }

    private Date(DateTime value)
         => Value = value;

    public static Date Create()
    {
        return new(DateTime.UtcNow);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}