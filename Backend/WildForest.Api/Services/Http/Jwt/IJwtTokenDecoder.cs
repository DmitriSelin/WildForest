using ErrorOr;

namespace WildForest.Api.Services.Http.Jwt
{
    public interface IJwtTokenDecoder
    {
        ErrorOr<Guid> GetUserIdFromToken(HttpRequest? request);
    }
}