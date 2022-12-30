using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Marks.ValueObjects
{
    public sealed class MarkDetails : ValueObject
    {
        public byte Value { get; }

        public string Comment { get; }

        private MarkDetails(byte value, string comment)
        {
            Value = value;
            Comment = comment;
        }

        public static MarkDetails CreateMarkDetails(byte value, string comment)
        {
            return new(value, comment);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Comment;
        }
    }
}