using WildForest.Application.Common.Models;

namespace WildForest.Application.Maps.Queries.GetCitiesList.Dto;

public sealed record ProfileCredentials(IEnumerable<NamedDto> Cities, IEnumerable<NamedDto> Languages);