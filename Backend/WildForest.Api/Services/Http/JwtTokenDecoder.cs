using ErrorOr;
using WildForest.Domain.Common.Exceptions;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace WildForest.Api.Services.Http
{
    public sealed class JwtTokenDecoder : IJwtTokenDecoder
    {
        public ErrorOr<Guid> GetUserIdFromToken(HttpRequest? request)
        {
            var userId = Guid.Empty;

            if (request is not null)
            {
                request.Headers.TryGetValue("Authorization", out StringValues bearer);

                if (bearer.Any())
                {
                    string? token = bearer[0]?.Split(" ")[1];

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var jwt = tokenHandler.ReadJwtToken(token);

                    userId = Guid.Parse(jwt.Claims.First(c => c.Type.Equals("sub")).Value);
                }
            }

            if (userId == Guid.Empty)
            {
                return Errors.Authentication.InvalidAuthorizationHeader;
            }

            return userId;
        }
    }
}