using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed class WeatherDescription
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    public WeatherDescription(string name, string description)
    {
        Name = name;
        Description = description;
    }
}