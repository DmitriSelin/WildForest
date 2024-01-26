using WildForest.Dto.Models;

namespace WildForest.Contracts.Maps;

public sealed class CityResponse : NamedDto
{
    public CityResponse(Guid id, string name) : base(id, name) {}
}