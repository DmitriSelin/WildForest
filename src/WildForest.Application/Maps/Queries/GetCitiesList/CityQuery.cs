using WildForest.Dto.Models;

namespace WildForest.Application.Maps.Queries.GetCitiesList;

public sealed class CityQuery : NamedDto
{
    public CityQuery(Guid id, string name) : base(id, name) {}
}