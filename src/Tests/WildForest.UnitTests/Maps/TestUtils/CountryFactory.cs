using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.Entities;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.UnitTests.Maps.TestUtils;

public sealed class CountryFactory
{
    public static Country Create()
    {
        var countryName = CountryName.Create("TestCountry");
        var country = Country.Create(countryName);

        var cityName = CityName.Create("TestCity");
        var location = Location.Create(-31.9522, 115.861);
        var city = City.Create(cityName, location, country.Id);
        country.AddCity(city);

        return country;
    }
}