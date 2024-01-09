using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Authentication.Commands.Profile;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCitiesList.Dto;
using WildForest.Contracts.Authentication;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/users")]
public sealed class UserController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IProfileService _profileService;
    private readonly ICitiesListQueryHandler _citiesListQueryHandler;

    public UserController(
        IMapper mapper,
        IProfileService profileService,
        ICitiesListQueryHandler citiesListQueryHandler)
    {
        _mapper = mapper;
        _profileService = profileService;
        _citiesListQueryHandler = citiesListQueryHandler;
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        ErrorOr<Guid> userId = HttpContext.GetUserIdFromAuthHeader();

        if (userId.IsError)
            return Problem(userId.Errors);

        string iPAddress = HttpContext.GetIpAddress();
        var command = _mapper.Map<UpdateProfileCommand>((request, iPAddress, userId));

        var authenticationResult = await _profileService.UpdateAsync(command);

        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        HttpContext.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);

        return Ok(response);
    }

    [HttpGet("languages-cities")]
    public async Task<IActionResult> GetLanguagesAndCities()
    {
        ErrorOr<Guid> userId = HttpContext.GetUserIdFromAuthHeader();

        if (userId.IsError)
            return Problem(userId.Errors);

        ProfileCredentials credentials = await _citiesListQueryHandler.GetCitiesAndLanguagesAsync(userId.Value);
        return Ok(credentials);
    }
}