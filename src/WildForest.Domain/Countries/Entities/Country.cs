using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Common.Models;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Domain.Countries.Entities;

public class Country : Entity<CountryId>
{
    public CountryName Name { get; } = null!;

    private readonly List<City> _cities = new();

    public IReadOnlyList<City> Cities => _cities.AsReadOnly();

    public void AddCity(City city)
    {
        _cities.Add(city);
    }

    public static Country Create(CountryName name)
        => new(CountryId.Create(), name);

    private Country(CountryId id, CountryName name) : base(id)
        => Name = name;

#pragma warning disable IDE0051 // Remove unused private members
    private Country(CountryId id) : base(id) { }
#pragma warning restore IDE0051 // Remove unused private members
}