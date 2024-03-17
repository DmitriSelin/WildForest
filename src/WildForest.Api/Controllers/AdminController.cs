using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Maps.Commands.AddCountry;
using ErrorOr;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;
using WildForest.Application.Weather.Commands.AddWeatherForecasts;

namespace WildForest.Api.Controllers;

[Route("api/admin")]
[Authorize(Roles = "Admin")]
public sealed class AdminController : ApiController
{
    private readonly ICountryCommandHandler _countryCommandHandler;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWeatherForecastDbService _weatherForecastDbService;

    public AdminController(
        ICountryCommandHandler countryCommandHandler,
        IUnitOfWork unitOfWork,
        IWeatherForecastDbService weatherForecastDbService)
    {
        _countryCommandHandler = countryCommandHandler;
        _unitOfWork = unitOfWork;
        _weatherForecastDbService = weatherForecastDbService;
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
            return Problem(result.Errors);

        return Ok(result.Value);
    }
}