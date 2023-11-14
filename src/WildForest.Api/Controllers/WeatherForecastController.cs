using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/forecasts")]
public sealed class WeatherForecastController : ApiController
{
    private readonly IHomeWeatherForecastService _homeWeatherForecastService;

    public WeatherForecastController(
        IHomeWeatherForecastService homeWeatherForecastService)
    {
        _homeWeatherForecastService = homeWeatherForecastService;
    }

    [HttpGet("homeCity/{date}")]
    public async Task<IActionResult> GetHomeWeatherForecasts(string date)
    {
        ErrorOr<Guid> userId = HttpContext.GetUserIdFromAuthHeader();

        if (userId.IsError)
            return Problem(userId.Errors);

        var currentDate = DateOnly.Parse(date);

        var query = new HomeWeatherForecastQuery(userId.Value, currentDate);

        var forecasts = await _homeWeatherForecastService.GetWeatherForecastsAsync(query);

        if (forecasts.IsError)
            return Problem(forecasts.Errors);

        return Ok(forecasts.Value);
    }
}