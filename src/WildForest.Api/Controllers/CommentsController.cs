using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Comments.Commands.Services;
using WildForest.Application.Comments.Common;
using WildForest.Application.Comments.Queries.GetComments;
using WildForest.Contracts.Comments;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/comments")]
public sealed class CommentsController : ApiController
{
    private readonly ICommentQueryHandler _commentQueryHandler;
    private readonly ICommentService _commentService;

    public CommentsController(ICommentQueryHandler commentQueryHandler, ICommentService commentService)
    {
        _commentQueryHandler = commentQueryHandler;
        _commentService = commentService;
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

    [HttpPost]
    public async Task<IActionResult> AddComment(CommentRequest request)
    {
        var userId = HttpContext.GetUserIdFromAuthHeader();

        if (userId.IsError)
            return Problem(userId.Errors);

        var command = new CommentCommand(userId.Value, request.WeatherForecastId, request.Text);

        ErrorOr<CommentDto> commentResult = await _commentService.AddCommentAsync(command);

        if (commentResult.IsError)
            return Problem(commentResult.Errors);

        return Ok(commentResult.Value);
    }
}