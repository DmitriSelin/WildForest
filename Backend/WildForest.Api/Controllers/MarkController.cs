using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Marks.Commands.LeaveComment;
using WildForest.Application.Marks.Commands.PutRating;
using WildForest.Contracts.Marks;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/marks")]
public sealed class MarkController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ICommentCommandHandler _commentCommandHandler;
    private readonly IRatingCommandHandler _ratingCommandHandler;

    public MarkController(
        IMapper mapper,
        ICommentCommandHandler commentCommandHandler,
        IRatingCommandHandler ratingCommandHandler)
    {
        _mapper = mapper;
        _commentCommandHandler = commentCommandHandler;
        _ratingCommandHandler = ratingCommandHandler;
    }

    [HttpPost("comment")]
    public async Task<IActionResult> LeaveComment(CommentRequest request)
    {
        var command = _mapper.Map<CommentCommand>(request);

        var comment = await _commentCommandHandler.LeaveCommentAsync(command);

        if (comment.IsError)
            return Problem(comment.Errors);

        return Ok(comment.Value);
    }

    [HttpPost("rating")]
    public async Task<IActionResult> PutRating(RatingRequest request)
    {
        var command = _mapper.Map<RatingCommand>(request);

        var mark = await _ratingCommandHandler.PutRatingAsync(command);

        if (mark.IsError)
            return Problem(mark.Errors);

        return Ok(mark.Value);
    }
}