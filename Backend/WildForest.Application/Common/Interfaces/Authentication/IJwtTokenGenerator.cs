using WildForest.Domain.Clients.Models;

namespace WildForest.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Person person);
}