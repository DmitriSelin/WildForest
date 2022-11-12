using WildForest.Domain.User.Entities;

namespace WildForest.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);
}