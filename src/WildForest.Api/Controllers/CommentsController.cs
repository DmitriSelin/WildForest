using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Api.Common.Extensions;
using WildForest.Application.Comments.Commands.Services;
using WildForest.Application.Comments.Common;
using WildForest.Contracts.Comments;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/comments")]
public sealed class CommentsController : ApiController
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{weatherId}")]
    public async Task<IActionResult> GetCommentsByWeatherForecastId(Guid weatherId)
    {
        var weatherForecastId = WeatherForecastId.Create(weatherId);

        IEnumerable<CommentDto> comments = await _commentService.GetCommentsAsync(weatherForecastId);

        return Ok(comments);
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

    [HttpPut]
    public async Task<IActionResult> UpdateComment(CommentRequestForUpdate request)
    {
        var userId = HttpContext.GetUserIdFromAuthHeader();

        if (userId.IsError)
            return Problem(userId.Errors);

        var command = new CommentCommandForUpdate(request.Id, request.WeatherForecastId, userId.Value, request.Text);

        ErrorOr<CommentDto> commentResult = await _commentService.UpdateCommentAsync(command);

        if (commentResult.IsError)
            return Problem(commentResult.Errors);

        return Ok(commentResult.Value);
    }
}