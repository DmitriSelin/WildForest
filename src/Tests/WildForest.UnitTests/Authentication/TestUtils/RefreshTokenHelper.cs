using WildForest.Domain.Tokens.Entities;
using WildForest.Domain.Users.Entities;
using WildForest.Infrastructure.Authentication;
using WildForest.Infrastructure.Persistence.UoW;
using WildForest.UnitTests.TestUtils;

namespace WildForest.UnitTests.Authentication.TestUtils;

public static class RefreshTokenHelper
{
    public static void Initialize(IEnumerable<User> users, UnitOfWork unitOfWork)
    {
        var refreshTokens = new List<RefreshToken>();
        var refreshTokenGenerator = new RefreshTokenGenerator(unitOfWork);

        foreach(var user in users)
        {
            var token = refreshTokenGenerator.GenerateTokenAsync(user.Id, Constants.User.IP).Result;
            refreshTokens.Add(token);
        }

        unitOfWork.Context.AddRange(refreshTokens);
    }
}