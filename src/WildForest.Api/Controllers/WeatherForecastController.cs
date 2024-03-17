using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WildForest.Api.Common.Extensions;
using WildForest.Api.SignalR.Hubs;
using WildForest.Application.Weather.Common.Models;
using WildForest.Application.Weather.Queries.GetHomeWeatherForecast;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/forecasts")]
public sealed class WeatherForecastController : ApiController
{
    private readonly IHomeWeatherForecastService _homeWeatherForecastService;
    private readonly IHubContext<ChatHub> _chatHub;

    public WeatherForecastController(
        IHomeWeatherForecastService homeWeatherForecastService,
        IHubContext<ChatHub> chatHub)
    {
        _homeWeatherForecastService = homeWeatherForecastService;
        _chatHub = chatHub;
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

        await AddToGroupAsync(forecasts.Value, currentDate);

        return Ok(forecasts.Value);
    }

    private async Task AddToGroupAsync(List<WeatherForecastResponse> weatherForecasts, DateOnly currentDate)
    {
        var weatherForecast = weatherForecasts.Single(x => x.Date == currentDate);
        await _chatHub.Groups.AddToGroupAsync(HttpContext.Connection.Id, weatherForecast.WeatherForecastId.ToString());
    }
}