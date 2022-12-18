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

        public WeatherDetails(
            string temperature,
            string description,
            string humidity,
            string pressure,
            string wind,
            string cloudness,
            string storm)
        {
            Temperature = temperature;
            Description = description;
            Humidity = humidity;
            Pressure = pressure;
            Wind = wind;
            Cloudness = cloudness;
            Storm = storm;
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