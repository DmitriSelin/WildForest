using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using WildForest.Application.Common.Interfaces.Authentication;

namespace WildForest.Infrastructure.Authentication
{
    public class JwtTokenDecoder : IJwtTokenDecoder
    {
        public Guid GetUserIdFromToken(HttpRequest? request)
        {
            var userId = Guid.Empty;

            if (request is not null)
            {
                request.Headers.TryGetValue("Authorization", out StringValues bearer);

                if (bearer.Any())
                {
                    string token = bearer[0].Split(" ")[1];

                    var tokenHandler = new JwtSecurityTokenHandler();

                    var jwt = tokenHandler.ReadJwtToken(token);

                    userId = Guid.Parse(jwt.Claims.First(c => c.Type.Equals("sub")).Value);
                }
            }

            return userId;
        }
    }
}