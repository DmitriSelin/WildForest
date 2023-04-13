using ErrorOr;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;

namespace WildForest.Application.Common.Interfaces.Services;

public interface IMarkService
{
    Task<ErrorOr<WeatherMark>> ChangeMediumMarkAsync(WeatherId weatherId, Rating rating);
}