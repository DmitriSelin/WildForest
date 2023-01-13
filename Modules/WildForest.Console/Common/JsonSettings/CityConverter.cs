using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;
using WildForest.Domain.Countries.ValueObjects;

namespace WildForest.Console.Common.JsonSettings
{
    public class CityConverter : JsonConverter<List<City>>
    {
        private readonly CountryId _countryId;

        public CityConverter(CountryId countryId)
        {
            _countryId = countryId;
        }

        public override List<City>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string cityName = string.Empty;
            double latitude = 0;
            double longitude = 0;
            int count = 0;

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
                            string? name = reader.GetString();
                            
                            if (name != null)
                                cityName = name;

                            count++;
                            break;

                        case "lat" when reader.TokenType == JsonTokenType.String:
                            latitude = double.Parse(reader.GetString().Replace(".", ","));
                            count++;
                            break;

                        case "lng" when reader.TokenType == JsonTokenType.String:
                            longitude = double.Parse(reader.GetString().Replace(".", ","));
                            count++;
                            break;

                        default:
                            continue;
                    }

                    if (count % 3 == 0)
                    {
                        var location = Location.CreateLocation(latitude, longitude);
                        var city = City.CreateCity(cityName, location, _countryId);

                        cities.Add(city);
                    }
                }              
            }

            return cities;
        }

        public override void Write(Utf8JsonWriter writer, List<City> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}