using ErrorOr;
using WildForest.Domain.Weather.ValueObjects;
using WildForest.Domain.WeatherMarks.Entities;
using WildForest.Domain.WeatherMarks.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Services;

public interface IMarkService
{
    Task<ErrorOr<WeatherMark>> ChangeMediumMarkAsync(WeatherId weatherId, MediumMark mediumMark);
}