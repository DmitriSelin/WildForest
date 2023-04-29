using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Common.Interfaces.Services;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;
using WildForest.Domain.Marks.ValueObjects;

namespace WildForest.Infrastructure.Services;

public sealed class MarkService : IMarkService
{
    private readonly IWeatherMarkRepository _weatherMarkRepository;

    public MarkService(IWeatherMarkRepository weatherMarkRepository)
    {
        _weatherMarkRepository = weatherMarkRepository;
    }

    public async Task<ErrorOr<WeatherMark>> ChangeMediumMarkAsync(WeatherId weatherId, Rating rating)
    {
        var weatherMark = await _weatherMarkRepository.GetWeatherMarkByWeatherIdAsync(weatherId);

        if (weatherMark is null)
            return Errors.WeatherMark.NotFound;

        double sum = CalculateSumOfMarks(weatherMark);
        double avgMark = CalculateNewAvgMark(sum, weatherMark, rating);

        weatherMark.Update(avgMark);
        await _weatherMarkRepository.UpdateWeatherMarkAsync(weatherMark);

        return weatherMark;
    }

    private static double CalculateSumOfMarks(WeatherMark weatherMark)
    {
        double sum = weatherMark.MediumMark.Value * weatherMark.CountOfMarks.Value;

        return sum;
    }

    private static double CalculateNewAvgMark(double sum, WeatherMark weatherMark, Rating rating)
    {
        sum += rating.Value;
        double avgMark = sum / (weatherMark.CountOfMarks.Value + 1);

        return avgMark;
    }
}