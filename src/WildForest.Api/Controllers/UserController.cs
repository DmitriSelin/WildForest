using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Api.Services.Http.Jwt;
using WildForest.Application.Authentication.Commands.Profile;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/users")]
public sealed class UserController : ApiController
{
    private readonly IJwtTokenDecoder _jwtTokenDecoder;
    private readonly IMapper _mapper;
    private readonly IProfileService _profileService;

    public UserController(
        IJwtTokenDecoder jwtTokenDecoder,
        IMapper mapper,
        IProfileService profileService)
    {
        _jwtTokenDecoder = jwtTokenDecoder;
        _mapper = mapper;
        _profileService = profileService;
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(RegisterRequest request)
    {
        var bearer = HttpContext.GetJwtTokenFromAuthHeader();
        ErrorOr<Guid> userId = _jwtTokenDecoder.GetUserIdFromToken(bearer);

        if (userId.IsError)
            return Problem(userId.Errors);

        string iPAddress = HttpContext.GetIpAddress();
        var command = _mapper.Map<UpdateProfileCommand>((request, iPAddress, userId));

        var authenticationResult = await _profileService.UpdateAsync(command);

        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);

        return Ok(response);
    }
}