using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Domain.Countries.Entities
{
    public class Country : Entity<CountryId>
    {
        public string Name { get; }

        private readonly List<City> _cities = new();

        public IReadOnlyList<City> Cities => _cities.AsReadOnly(); 

        private Country(CountryId id, string name) : base(id)
        {
            Name = name;
        }

        public static Country CreateCountry(string name)
        {
            return new(CountryId.CreateCountryId(), name);
        }
    }
}