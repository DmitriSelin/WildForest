using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Humidity : ValueObject
    {
        public byte Value { get; }

        private Humidity(byte value) 
            => Value = value;

        public static Humidity CreateHumidity(byte value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}