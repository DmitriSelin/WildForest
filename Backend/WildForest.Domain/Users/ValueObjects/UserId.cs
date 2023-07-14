using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Users.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    private UserId(Guid value)
        => Value = value;

    public static UserId Create()
        => new(Guid.NewGuid());

    public static UserId Create(Guid value)
        => new(value);

    public static UserId Parse(string value)
        => Create(Guid.Parse(value));

    public override string ToString()
        => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}