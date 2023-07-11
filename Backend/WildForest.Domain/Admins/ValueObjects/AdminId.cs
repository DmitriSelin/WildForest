using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Admins.ValueObjects;

public sealed class AdminId : ValueObject
{
    public Guid Value { get; }

    private AdminId(Guid value)
        => Value = value;

    public static AdminId Create()
        => new(Guid.NewGuid());

    public static AdminId Create(Guid value)
        => new(value);

    public static AdminId Parse(string value)
        => new(Guid.Parse(value));

    public override string ToString()
        => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}