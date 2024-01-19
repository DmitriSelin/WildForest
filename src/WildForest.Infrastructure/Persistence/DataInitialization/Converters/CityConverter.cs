using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Application.Common.Extensions;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Infrastructure.Persistence.DataInitialization.Converters;

public sealed class CityConverter : JsonConverter<List<City>>
{
    private readonly CountryId _countryId;

    public CityConverter(CountryId countryId)
    {
        _countryId = countryId;
    }

    public override List<City> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string cityName = string.Empty;
        double latitude = 0;
        double longitude = 0;
        bool isFilled = false;

        List<City> cities = new();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "city" when reader.TokenType == JsonTokenType.String:

                        cityName = reader.GetString()!;
                        break;

                    case "lat" when reader.TokenType == JsonTokenType.String:

                        latitude = double.Parse(reader.GetString()!.Replace(".", ","));
                        break;

                    case "lng" when reader.TokenType == JsonTokenType.String:

                        longitude = double.Parse(reader.GetString()!.Replace(".", ","));
                        isFilled = true;
                        break;

                    default:
                        continue;
                }

                if (isFilled)
                {
                    var city = CreateCity(cityName, latitude, longitude);
                    cities.Add(city);

                    isFilled = false;
                }
            }
        }

        return cities;
    }

    private City CreateCity(string cityName, double latitude, double longitude)
    {
        var name = CityName.Create(cityName);
        var location = Location.Create(latitude, longitude);
        var city = City.Create(name, location, _countryId);

        return city;
    }

    public override void Write(Utf8JsonWriter writer, List<City> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}