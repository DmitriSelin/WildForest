using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WildForest.Application.Ratings.Commands.Votes;
using WildForest.Application.Ratings.Common;
using WildForest.Contracts.Ratings;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/rating")]
public sealed class RatingController : ApiController
{
    private readonly IVoteService _voteService;

    public RatingController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpPost("vote/up")]
    public async Task<IActionResult> UpVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateUpVote(request.RatingId, request.UserId);

        ErrorOr<RatingDto> rating = await _voteService.VoteAsync(command);

        return rating.Match(
            ratingDto => Ok(ratingDto),
            errors => Problem(errors));
    }

    [HttpPost("vote/down")]
    public async Task<IActionResult> DownVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateDownVote(request.RatingId, request.UserId);

        ErrorOr<RatingDto> rating = await _voteService.VoteAsync(command);

        return rating.Match(
            ratingDto => Ok(ratingDto),
            errors => Problem(errors));
    }
}