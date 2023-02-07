using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class Wind : ValueObject
    {
        public double Speed { get; }

        public int Direction { get; }

        public double Gust { get; }

        private Wind(double speed, int direction, double gust)
        {
            Speed = speed;
            Direction = direction;
            Gust = gust;
        }

        public static Wind Create(double speed, int direction, double gust)
        {
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