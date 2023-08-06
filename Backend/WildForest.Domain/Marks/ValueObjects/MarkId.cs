using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects;

public sealed class MarkId : ValueObject
{
    public Guid Value { get; }

    private MarkId(Guid value)
        => Value = value;

    public static MarkId Create()
        => new(Guid.NewGuid());

    public static MarkId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}