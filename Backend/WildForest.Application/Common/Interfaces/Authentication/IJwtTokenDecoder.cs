using Microsoft.AspNetCore.Http;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenDecoder
    {
        UserId GetUserIdFromToken(HttpRequest? request);
    }
}