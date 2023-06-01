using System.Text.Json.Serialization;

namespace WildForest.Frontend.Contracts.Weather.Models;

public sealed class Wind
{
    [JsonPropertyName("speed")]
    public double Speed { get; set; }

    [JsonPropertyName("direction")]
    public int Direction { get; set; }

    [JsonPropertyName("gust")]
    public double Gust { get; set; }

    public Wind(double speed, int direction, double gust)
    {
        Speed = speed;
        Direction = direction;
        Gust = gust;
    }
}