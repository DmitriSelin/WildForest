using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed class Temperature
{
    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("valueFeelsLike")]
    public double ValueFeelsLike { get; set; }

    public Temperature(double value, double valueFeelsLike)
    {
        Value = value;
        ValueFeelsLike = valueFeelsLike;
    }
}