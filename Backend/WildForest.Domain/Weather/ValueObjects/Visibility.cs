using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Visibility : ValueObject
    {
        public double Value { get; }

        private Visibility(double value) 
        {
            Value = value;
        }

        public static Visibility Create(double value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}