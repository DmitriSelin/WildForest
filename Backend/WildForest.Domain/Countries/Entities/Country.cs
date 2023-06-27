using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Domain.Countries.Entities
{
    public class Country : Entity<CountryId>
    {
        public CountryName Name { get; } = null!;

        private readonly List<City> _cities = new();

        public IReadOnlyList<City> Cities => _cities.AsReadOnly(); 

        private Country(CountryId id, CountryName name) : base(id)
        {
            Name = name;
        }

        private Country(CountryId id) : base(id) { }

        public static Country Create(CountryName name)
        {
            return new(CountryId.Create(), name);
        }
    }
}