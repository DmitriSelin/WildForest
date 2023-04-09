using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.ValueObjects;

namespace WildForest.Domain.WeatherMarks.Entities;

public sealed class WeatherMark : Entity<WeatherMarkId>
{
    public MediumMark MediumMark { get; } = null!;

    public WeatherId WeatherId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    private WeatherMark(
        WeatherMarkId id,
        MediumMark mediumMark,
        WeatherId weatherId) : base(id)
    {
        MediumMark = mediumMark;
        WeatherId = weatherId;
    }

    public static WeatherMark Create(MediumMark mediumMark, WeatherId weatherId)
    {
        return new(WeatherMarkId.Create(), mediumMark, weatherId);
    }

    private WeatherMark(WeatherMarkId id) : base(id) { }
}