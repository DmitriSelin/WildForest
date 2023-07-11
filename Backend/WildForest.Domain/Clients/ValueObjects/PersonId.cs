using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Clients.ValueObjects;

public sealed class PersonId : ValueObject
{
    public Guid Value { get; }

    private PersonId(Guid value)
        => Value = value;

    public static PersonId Create()
        => new(Guid.NewGuid());

    public static PersonId Create(Guid value)
        => new(value);

    public static PersonId Parse(string cityId)
        => Create(Guid.Parse(cityId));

    public override string ToString()
        => Value.ToString();

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}