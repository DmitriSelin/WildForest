using WildForest.Domain.Users.Entities;

namespace WildForest.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}