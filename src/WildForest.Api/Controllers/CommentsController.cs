using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Comments.Queries.GetComments;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/comments")]
public sealed class CommentsController : ApiController
{
    private readonly ICommentQueryHandler _commentQueryHandler;

    public CommentsController(ICommentQueryHandler commentQueryHandler)
    {
        _commentQueryHandler = commentQueryHandler;
    }

    [HttpGet("{weatherId}")]
    public async Task<IActionResult> GetCommentsByWeatherForecastId(Guid weatherId)
    {
        var weatherForecastId = WeatherForecastId.Create(weatherId);

        ErrorOr<IEnumerable<CommentDto>> commentsResult = await _commentQueryHandler.GetCommentsAsync(weatherForecastId);

        if (commentsResult.IsError)
            return Problem(commentsResult.Errors);

        return Ok(commentsResult.Value);
    }
}