using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in percentages
    /// </summary>
    public sealed class Humidity : ValueObject
    {
        public byte Value { get; }

        private Humidity(byte value) 
            => Value = value;

        public static Humidity Create(byte value)
        {
            if (value > 100)
                throw new ArgumentOutOfRangeException("Invalid humidity");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}