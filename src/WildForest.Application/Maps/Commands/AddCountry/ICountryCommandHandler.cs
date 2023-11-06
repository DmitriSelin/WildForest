using ErrorOr;

namespace WildForest.Application.Maps.Commands.AddCountry;

public interface ICountryCommandHandler
{
    Task<ErrorOr<string>> AddCountryAsync(CountryCommand command);
}