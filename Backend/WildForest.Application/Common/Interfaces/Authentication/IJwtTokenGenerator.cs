using WildForest.Domain.User.Entities;

namespace WildForest.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}