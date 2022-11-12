using WildForest.Domain.City.ValueObjects;
using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public class WeatherDetails : ValueObject
    {
        public string Temperature { get; }

        public string Description { get; }

        public string Humidity { get; }

        public string Pressure { get; }

        public string Wind { get; }

        public string Cloudness { get; }

        public string Storm { get; }

        public double Mark { get; }

        public CityId CityId { get; } = null!;

        public virtual City.Entities.City City { get; } = null!;

        public WeatherDetails(
            string temperature, 
            string description, 
            string humidity,
            string pressure, 
            string wind, 
            string cloudness,
            string storm,
            double mark,
            CityId cityId,
            City.Entities.City city)
        {
            Temperature = temperature;
            Description = description;
            Humidity = humidity;
            Pressure = pressure;
            Wind = wind;
            Cloudness = cloudness;
            Storm = storm;
            Mark = mark;
            CityId = cityId;
            City = city;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Temperature;
            yield return Description;
            yield return Humidity;
            yield return Pressure;
        }
    }
}