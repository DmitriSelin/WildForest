using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Maps.Commands.AddCountry;
using ErrorOr;

namespace WildForest.Api.Controllers;

[Route("api/maps")]
[Authorize(Roles = "Admin")]
public sealed class MapsController : ApiController
{
    private readonly ICountryCommandHandler _countryCommandHandler;

    public MapsController(ICountryCommandHandler countryCommandHandler)
    {
        _countryCommandHandler = countryCommandHandler;
    }

    [HttpPost("country")]
    public async Task<IActionResult> AddCountry(string countryName)
    {
        if (countryName.Length is < 3 or > 50)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Not correct countryName");
        }

        var command = new CountryCommand(countryName);

        ErrorOr<string> result = await _countryCommandHandler.AddCountryAsync(command);

        if (result.IsError)
        {
            return Problem(result.Errors);
        }

        return Ok(result.Value);
    }
}