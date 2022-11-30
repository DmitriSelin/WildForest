using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Cities.Entities
{
    public sealed class City : Entity<CityId>
    {
        public string Name { get; }

        public Location Location { get; }

        public City(CityId id, string name, Location location) : base(id)
        {
            Name = name;
            Location = location;
        }
    }
}