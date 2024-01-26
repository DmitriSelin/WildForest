using WildForest.Dto.Models;

namespace WildForest.Application.Authentication.Queries.Registration;

public sealed record AuthCredentials(
    IEnumerable<NamedDto> Countries,
    IEnumerable<NamedDto> Languages);