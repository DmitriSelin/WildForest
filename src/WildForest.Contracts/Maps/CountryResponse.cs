using WildForest.Dto.Models;

namespace WildForest.Contracts.Maps;

public sealed class CountryResponse : NamedDto
{
    public CountryResponse(Guid id, string name) : base(id, name) {}
}