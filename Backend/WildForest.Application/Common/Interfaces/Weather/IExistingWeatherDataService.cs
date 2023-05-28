using WildForest.Domain.Cities.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Weather;

public interface IExistingWeatherDataService
{
    Task CheckWeatherDataExisting(CityId cityId);
}