using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in millimeters of mercury
    /// </summary>
    public sealed class Pressure : ValueObject
    {
        public double Value { get; }

        private Pressure(double value)
            => Value = value;

        public static Pressure Create(double value)
        {
            if (value < 580 || value > 812)     //min and max values of pressure
                throw new ArgumentOutOfRangeException("Invalid pressure");

            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}