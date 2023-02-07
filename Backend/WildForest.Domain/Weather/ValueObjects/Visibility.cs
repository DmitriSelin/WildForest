using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in metres
    /// </summary>
    public sealed class Visibility : ValueObject
    {
        public double Value { get; }

        private Visibility(double value) 
        {
            Value = value;
        }

        public static Visibility Create(double value)
        {
            if (value < 0 || value > 10_000)
                throw new ArgumentOutOfRangeException("Invalid visibility");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}