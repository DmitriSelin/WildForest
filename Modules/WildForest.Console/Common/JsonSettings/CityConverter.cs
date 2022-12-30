using System.Text.Json;
using System.Text.Json.Serialization;
using WildForest.Domain.Cities.Entities;
using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Console.Common.JsonSettings
{
    public class CityConverter : JsonConverter<City>
    {
        public override City? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string cityName = string.Empty;
            double latitude = 0;
            double longitude = 0;

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

                            break;
                        case "lat" when reader.TokenType == JsonTokenType.String:
                            latitude = double.Parse(reader.GetString().Replace(".", ","));

                            break;
                        case "lng" when reader.TokenType == JsonTokenType.String:

                            longitude = double.Parse(reader.GetString().Replace(".", ","));
                            break;
                    }
                }
            }

            var location = Location.CreateLocation(latitude, longitude);

            return City.CreateCity(cityName, location);
        }

        public override void Write(Utf8JsonWriter writer, City value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}