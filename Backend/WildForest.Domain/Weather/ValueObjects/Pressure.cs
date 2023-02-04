using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Pressure : ValueObject
    {
        public double Value { get; }

        private Pressure(double value)
            => Value = value;

        public static Pressure CreatePressure(double value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}