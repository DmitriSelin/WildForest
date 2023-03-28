using System.ComponentModel.DataAnnotations;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects;

public class Comment : ValueObject
{
    public string Value { get; }

    private Comment(string value)
        => Value = value;

    public static Comment Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        string comment = value.Trim();

        if (comment.Length > 200)
            throw new ValidationException(nameof(value));

        return new(comment);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}