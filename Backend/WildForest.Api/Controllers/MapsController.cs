using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Maps.Commands.AddCountry;
using ErrorOr;
using MapsterMapper;
using WildForest.Application.Maps.Commands.AddCities;
using WildForest.Application.Maps.Queries.GetCountriesList;
using WildForest.Contracts.Maps;

namespace WildForest.Api.Controllers;

[Route("api/maps")]
[Authorize(Roles = "Admin")]
public sealed class MapsController : ApiController
{
    private readonly ICountryCommandHandler _countryCommandHandler;
    private readonly ICountriesListQueryHandler _countriesListQueryHandler;
    private readonly ICityCommandHandler _cityCommandHandler;
    private readonly IMapper _mapper;

    public MapsController(
        ICountryCommandHandler countryCommandHandler, 
        ICountriesListQueryHandler countriesListQueryHandler,
        ICityCommandHandler cityCommandHandler,
        IMapper mapper)
    {
        _countryCommandHandler = countryCommandHandler;
        _countriesListQueryHandler = countriesListQueryHandler;
        _cityCommandHandler = cityCommandHandler;
        _mapper = mapper;
    }

    [HttpPost("country")]
    public async Task<IActionResult> AddCountry(string countryName)
    {
        if (countryName.Length is < 3 or > 50)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Not correct country's name");
        }

        var command = new CountryCommand(countryName);

        ErrorOr<string> result = await _countryCommandHandler.AddCountryAsync(command);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result.Value);
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        List<CountryQuery> countries = await _countriesListQueryHandler.GetCountriesAsync();

        var countriesResponse = _mapper.Map<List<CountryResponse>>(countries);

        return Ok(countriesResponse);
    }

    [HttpPost("cities")]
    public async Task<IActionResult> AddCities(CityRequest request)
    {
        var command = new CityCommand(request.CountryId, request.FileName);

        ErrorOr<string> result = await _cityCommandHandler.AddCitiesFromJsonFileAsync(command);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result.Value);
    }
}