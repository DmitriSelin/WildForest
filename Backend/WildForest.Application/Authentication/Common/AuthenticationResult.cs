using WildForest.Domain.User.Entities;

namespace WildForest.Application.Authentication.Common
{
    public sealed record AuthenticationResult(
        User User,
        string Token);
}