using ErrorOr;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Authentication.Commands.RegisterUser;
using WildForest.Application.Authentication.Common;
using WildForest.Application.Authentication.Queries.LoginUser;
using WildForest.Application.Maps.Queries.GetCitiesList;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Contracts.Authentication;
using WildForest.Contracts.Maps;
using WildForest.Domain.Common.Errors;

namespace WildForest.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public sealed class AuthenticationController : ApiController
    {
        private readonly IUserRegistrator _userRegistrator;
        private readonly IUserLogger _userLogger;
        private readonly IMapper _mapper;
        private readonly ICountriesListQueryHandler _countriesListQueryHandler;
        private readonly ICitiesListQueryHandler _citiesListQueryHandler;

        public AuthenticationController(
            IUserRegistrator userRegistrator,
            IUserLogger userLogger,
            IMapper mapper,
            ICountriesListQueryHandler countriesListQueryHandler,
            ICitiesListQueryHandler citiesListQueryHandler)
        {
            _userRegistrator = userRegistrator;
            _userLogger = userLogger;
            _mapper = mapper;
            _countriesListQueryHandler = countriesListQueryHandler;
            _citiesListQueryHandler = citiesListQueryHandler;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            string iPAddress = HttpContext.GetIpAddress();
            var command = _mapper.Map<RegisterUserCommand>((request, iPAddress));

            ErrorOr<AuthenticationResult> authenticationResult = await _userRegistrator.RegisterAsync(command);
            
            if (authenticationResult.IsError)
            {
                return Problem(authenticationResult.Errors);
            }
            
            HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);
            
            var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            string iPAddress = HttpContext.GetIpAddress();
            var query = _mapper.Map<LoginUserQuery>((request, iPAddress));

            var authenticationResult = await _userLogger.LoginAsync(query);

            if (authenticationResult.IsError && authenticationResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authenticationResult.FirstError.Description);
            }

            if (authenticationResult.IsError)
            {
                return Problem(authenticationResult.Errors);
            }
            
            HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);

            var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);
            return Ok(response);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            List<CountryQuery> countries = await _countriesListQueryHandler.GetCountriesAsync();

            var countriesResponse = _mapper.Map<List<CountryResponse>>(countries);

            return Ok(countriesResponse);
        }

        [HttpGet("cities/{countryId}")]
        public async Task<IActionResult> GetCitiesByCountryId(Guid countryId)
        {
            var cities = await _citiesListQueryHandler.GetCitiesByCountryIdAsync(countryId);

            return cities.Match(
                citiesResponse => Ok(_mapper.Map<List<CityResponse>>(citiesResponse)),
                errors => Problem(errors));
        }

        [HttpPost("admins/register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterRequest request)
        {
            string iPAddress = HttpContext.GetIpAddress();
            var command = _mapper.Map<RegisterUserCommand>((request, iPAddress));

            ErrorOr<AuthenticationResult> authenticationResult =
                await _userRegistrator.RegisterAsync(command);
            
            if (authenticationResult.IsError)
            {
                return Problem(authenticationResult.Errors);
            }
            
            HttpContext.Response.Cookies.SetTokenCookie(authenticationResult.Value.RefreshToken);
            
            var response = _mapper.Map<AuthenticationResponse>(authenticationResult.Value);
            return Ok(response);
        }
    }
}