namespace WildForest.Application.Maps.Commands.AddCities;

public sealed record CityCommand(Guid CountryId, string FileName);