using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Wind : ValueObject
    {
        /// <summary>
        /// Value in meter/sec
        /// </summary>
        public double Speed { get; }

        /// <summary>
        /// Value in degrees
        /// </summary>
        public int Direction { get; }

        /// <summary>
        /// Value in meter/sec
        /// </summary>
        public double Gust { get; }

        private Wind(double speed, int direction, double gust)
        {
            Speed = speed;
            Direction = direction;
            Gust = gust;
        }

        public static Wind Create(double speed, int direction, double gust)
        {
            if (speed < 0 || speed > 113)   // max value of wind's speed
                throw new ArgumentOutOfRangeException("Invalid wind's speed");

            if (direction < 0 || direction > 360)   // 360 degrees In a circle
                throw new ArgumentOutOfRangeException("Invalid direction");

            if (gust < 0 || gust > 113)   // max value of wind's speed
                throw new ArgumentOutOfRangeException("Invalid wind's speed");

            return new(speed, direction, gust);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Speed;
            yield return Direction; 
            yield return Gust;
        }
    }
}