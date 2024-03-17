using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Comments.ValueObjects;

public sealed class CommentId : ValueObject
{
    public Guid Value { get; }

    private CommentId(Guid value)
        => Value = value;

    public static CommentId Create()
        => new(Guid.NewGuid());

    public static CommentId Create(Guid value)
        => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}