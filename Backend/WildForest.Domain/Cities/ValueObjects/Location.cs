using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Cities.ValueObjects
{
    public sealed class Location : ValueObject
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public Location(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}