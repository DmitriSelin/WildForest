using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Commands.Registration.Commands;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Contracts.Authentication;
using WildForest.Contracts.Maps;
using WildForest.Domain.Common.Errors;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/auth")]
public sealed class AuthenticationController : ApiController
{
    private readonly IRegistrationService _registrationService;
    private readonly ILoginService _loginService;
    private readonly IMapper _mapper;
    private readonly ICountriesListQueryHandler _countriesListQueryHandler;
    private readonly ICitiesListQueryHandler _citiesListQueryHandler;

    public AuthenticationController(
        IRegistrationService registrationService,
        ILoginService loginService,
        IMapper mapper,
        ICountriesListQueryHandler countriesListQueryHandler,
        ICitiesListQueryHandler citiesListQueryHandler)
    {
        _registrationService = registrationService;
        _loginService = loginService;
        _mapper = mapper;
        _countriesListQueryHandler = countriesListQueryHandler;
        _citiesListQueryHandler = citiesListQueryHandler;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        string iPAddress = HttpContext.GetIpAddress();
        var command = _mapper.Map<RegisterCommand>((request, iPAddress));

        ErrorOr<AuthenticationResult> authenticationResult = await _registrationService.RegisterAsync(command);

        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        string iPAddress = HttpContext.GetIpAddress();
        var query = _mapper.Map<LoginQuery>((request, iPAddress));

        ErrorOr<AuthenticationResult> authenticationResult = await _loginService.LoginAsync(query);

        if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authenticationResult.FirstError.Description);
        }

        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        List<CountryQuery> countries = await _countriesListQueryHandler.GetCountriesAsync();

        var countriesResponse = _mapper.Map<List<CountryResponse>>(countries);

        return Ok(countriesResponse);
    }

    [AllowAnonymous]
    [HttpGet("cities/{countryId}")]
    public async Task<IActionResult> GetCitiesByCountryId(Guid countryId)
    {
        var cities = await _citiesListQueryHandler.GetCitiesByCountryIdAsync(countryId);

        return cities.Match(
            citiesResponse => Ok(_mapper.Map<List<CityResponse>>(citiesResponse)),
            errors => Problem(errors));
    }

    [HttpPost("admins/register")]
    public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
    {
        string iPAddress = HttpContext.GetIpAddress();
        var command = _mapper.Map<RegisterCommand>((request, iPAddress));

        ErrorOr<AuthenticationResult> authenticationResult =
            await _registrationService.RegisterAsync(command: command, isUserRole: false);

        if (authenticationResult.IsError)
            return Problem(authenticationResult.Errors);

        HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

        var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);
        return Ok(response);
    }
}