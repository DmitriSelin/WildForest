using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Mark.ValueObjects
{
    public class MarkDetails : ValueObject
    {
        public byte Value { get; }

        public string Comment { get; }

        public MarkDetails(byte value, string comment)
        {
            Value = value;
            Comment = comment;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Comment;
        }
    }
}