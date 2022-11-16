using WildForest.Domain.Common.Models;

namespace WildForest.Domain.City.ValueObjects
{
    public sealed class Location : ValueObject
    {
        public string Country { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public Location(string country, double latitude, double longitude)
        {
            Country = country;
            Latitude = latitude;
            Longitude = longitude;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return Latitude;
            yield return Longitude;
        }
    }
}