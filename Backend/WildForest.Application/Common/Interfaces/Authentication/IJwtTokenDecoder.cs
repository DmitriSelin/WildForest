using Microsoft.AspNetCore.Http;

namespace WildForest.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenDecoder
    {
        Guid GetUserIdFromToken(HttpRequest? request);
    }
}