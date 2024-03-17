using WildForest.Dto.Models;

namespace WildForest.Application.Maps.Queries.GetCountriesList;

public sealed class CountryQuery : NamedDto
{
    public CountryQuery(Guid id, string name) : base(id, name) {}
}