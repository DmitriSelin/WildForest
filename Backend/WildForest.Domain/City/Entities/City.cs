using WildForest.Domain.City.ValueObjects;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.City.Entities
{
    public class City : Entity<CityId>
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