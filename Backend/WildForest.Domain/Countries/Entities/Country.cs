using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Domain.Countries.Entities
{
    public class Country : Entity<CountryId>
    {
        public string Name { get; }

        public virtual ICollection<City> Cities { get; } = null!;

        public Country(CountryId id, string name) : base(id)
        {
            Name = name;
        }
    }
}