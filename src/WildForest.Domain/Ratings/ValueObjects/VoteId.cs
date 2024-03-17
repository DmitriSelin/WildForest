using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Ratings.ValueObjects;

public sealed class VoteId : ValueObject
{
    public Guid Value { get; }

    private VoteId(Guid value)
        => Value = value;

    public static VoteId Create()
        => new(Guid.NewGuid());

    public static VoteId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}