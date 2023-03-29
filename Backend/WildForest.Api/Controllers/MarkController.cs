using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Marks.Commands.LeaveComment;
using WildForest.Contracts.Marks;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/marks")]
public sealed class MarkController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ICommentCommandHandler _commentCommandHandler;

    public MarkController(IMapper mapper, ICommentCommandHandler commentCommandHandler)
    {
        _mapper = mapper;
        _commentCommandHandler = commentCommandHandler;
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
}