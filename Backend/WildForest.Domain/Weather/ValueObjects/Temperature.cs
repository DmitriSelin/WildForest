using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Temperature : ValueObject
    {
        public double Value { get; }

        public double ValueFeelsLike { get; }

        private Temperature(double value, double valueFeelsLike)
        {
            Value = value;
            ValueFeelsLike = valueFeelsLike;
        }

        public static Temperature Create(double value, double valueFeelsLike)
        {
            return new(value, valueFeelsLike);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return ValueFeelsLike;
        }
    }
}