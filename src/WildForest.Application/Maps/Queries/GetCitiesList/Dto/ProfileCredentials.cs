using WildForest.Dto.Models;

namespace WildForest.Application.Maps.Queries.GetCitiesList.Dto;

public sealed record ProfileCredentials(IEnumerable<NamedDto> Cities, IEnumerable<NamedDto> Languages);