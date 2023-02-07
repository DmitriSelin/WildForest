using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in percentages
    /// </summary>
    public sealed class Cloudiness : ValueObject
    {
        public byte Value { get; }

        private Cloudiness(byte value)
            => Value = value;

        public static Cloudiness Create(byte value)
        {
            if (value > 100)
                throw new ArgumentOutOfRangeException("Invalid cloudiness");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}