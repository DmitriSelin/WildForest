using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using WildForest.Application.Common.Interfaces.Authentication;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Infrastructure.Authentication
{
    public class JwtTokenDecoder : IJwtTokenDecoder
    {
        public UserId GetUserIdFromToken(HttpRequest? request)
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
                throw new Exception("Not correct Authorization Header");
            }

            return UserId.CreateUserId(userId);
        }
    }
}