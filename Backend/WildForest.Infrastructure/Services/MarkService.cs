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

        weatherMark.CountOfMarks.Increment();

        double sum = CalculateSumOfMarks(weatherMark, rating);
        double avgMark = sum / weatherMark.CountOfMarks.Value;

        weatherMark.MediumMark.Update(avgMark);

        await _weatherMarkRepository.UpdateWeatherMarkAsync(weatherMark);

        return weatherMark;
    }

    private static double CalculateSumOfMarks(WeatherMark weatherMark, Rating rating)
    {
        double sum = weatherMark.CountOfMarks.Value * weatherMark.MediumMark.Value;
        sum += rating.Value;

        return sum;
    }
}