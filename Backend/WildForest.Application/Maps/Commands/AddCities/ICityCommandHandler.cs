using ErrorOr;

namespace WildForest.Application.Maps.Commands.AddCities;

public interface ICityCommandHandler
{
    Task<ErrorOr<string>> AddCitiesFromJsonFileAsync(CityCommand command);
}