using WildForest.Domain.Common.Models;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.ValueObjects;

namespace WildForest.Domain.WeatherMarks.Entities;

public sealed class WeatherMark : Entity<WeatherMarkId>
{
    public MediumMark MediumMark { get; } = null!;

    public CountOfMarks CountOfMarks { get; } = null!;

    public WeatherId WeatherId { get; } = null!;

    public WeatherForecast WeatherForecast { get; } = null!;

    public void Update(double newMediumMark)
    {
        CountOfMarks.Increment();
        MediumMark.Update(newMediumMark);
    }

    private WeatherMark(
        WeatherMarkId id,
        MediumMark mediumMark,
        CountOfMarks countOfMarks,
        WeatherId weatherId) : base(id)
    {
        MediumMark = mediumMark;
        CountOfMarks = countOfMarks;
        WeatherId = weatherId;
    }

    public static WeatherMark Create(
        MediumMark mediumMark,
        CountOfMarks countOfMarks,
        WeatherId weatherId)
    {
        return new(
            WeatherMarkId.Create(),
            mediumMark,
            countOfMarks,
            weatherId);
    }

    private WeatherMark(WeatherMarkId id) : base(id) { }
}