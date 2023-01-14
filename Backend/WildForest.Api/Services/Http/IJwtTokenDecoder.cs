using ErrorOr;

namespace WildForest.Api.Services.Http
{
    public interface IJwtTokenDecoder
    {
        ErrorOr<Guid> GetUserIdFromToken(HttpRequest? request);
    }
}