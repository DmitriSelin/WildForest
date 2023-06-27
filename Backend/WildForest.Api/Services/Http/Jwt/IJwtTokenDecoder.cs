using ErrorOr;
using Microsoft.Extensions.Primitives;

namespace WildForest.Api.Services.Http.Jwt
{
    public interface IJwtTokenDecoder
    {
        ErrorOr<Guid> GetUserIdFromToken(StringValues bearer);
    }
}