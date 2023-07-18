using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Api.Services.Http.Jwt;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/forecasts")]
public sealed class WeatherForecastController : ApiController
{
    private readonly IJwtTokenDecoder _jwtTokenDecoder;
    private readonly IHomeWeatherForecastService _homeWeatherForecastService;

    public WeatherForecastController(
        IJwtTokenDecoder jwtTokenDecoder,
        IHomeWeatherForecastService homeWeatherForecastService)
    {
        _jwtTokenDecoder = jwtTokenDecoder;
        _homeWeatherForecastService = homeWeatherForecastService;
    }

    [HttpGet("homeCity/{date}")]
    public async Task<IActionResult> GetHomeWeatherForecasts(string date)
    {
        var bearer = HttpContext.GetJwtTokenFromAuthHeader();
        ErrorOr<Guid> userId = _jwtTokenDecoder.GetUserIdFromToken(bearer);

        if (userId.IsError)
            return Problem(userId.Errors);

        var forecastDate = DateOnly.Parse(date);
        var query = new HomeWeatherForecastQuery(userId.Value, forecastDate);

        var forecasts = await _homeWeatherForecastService.GetWeatherForecastsAsync(query);

        if (forecasts.IsError)
            return Problem(forecasts.Errors);

        return Ok(forecasts.Value);
    }
}