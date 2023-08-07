using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Marks.Commands.Votes;
using WildForest.Application.Marks.Common;
using WildForest.Contracts.Marks;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/marks")]
public sealed class MarksController : ApiController
{
    private readonly IVoteService _voteService;

    public MarksController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost("vote/up")]
    public async Task<IActionResult> UpVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateUpVote(request.MarkId, request.UserId);

        ErrorOr<MarkDto> mark = await _voteService.VoteAsync(command);

        return mark.Match(
            markDto => Ok(mark),
            errors => Problem(errors));
    }

    [HttpPost("vote/down")]
    public async Task<IActionResult> DownVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateDownVote(request.MarkId, request.UserId);

        ErrorOr<MarkDto> mark = await _voteService.VoteAsync(command);

        return mark.Match(
            markDto => Ok(mark),
            errors => Problem(errors));
    }
}