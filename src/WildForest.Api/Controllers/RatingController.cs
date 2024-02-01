using ErrorOr;
using MapsterMapper;
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
    private readonly IMapper _mapper;

    public RatingController(IVoteService voteService, IMapper mapper)
    {
        _voteService = voteService;
        _mapper = mapper;
    }

    [HttpPost("vote")]
    public async Task<IActionResult> GetVote(VoteCreationRequest request)
    {
        ErrorOr<VoteDto> vote = await _voteService.GetVoteAsync(request.RatingId, request.UserId);

        return vote.Match(
            voteDto => Ok(voteDto),
            errors => Problem(errors));
    }

    [HttpPost("vote/up")]
    public async Task<IActionResult> UpVote(VoteCreationRequest request)
    {
        var command = new VoteCreationCommand(request.RatingId, request.UserId, VoteConstants.Up);

        ErrorOr<RatingDto> rating = await _voteService.VoteAsync(command);

        return ReturnActionResult(rating);
    }

    [HttpPost("vote/down")]
    public async Task<IActionResult> DownVote(VoteCreationRequest request)
    {
        var command = new VoteCreationCommand(request.RatingId, request.UserId, VoteConstants.Down);

        ErrorOr<RatingDto> rating = await _voteService.VoteAsync(command);

        return ReturnActionResult(rating);
    }

    [HttpPut("vote")]
    public async Task<IActionResult> UpdateVote(VoteUpdationRequest request)
    {
        var command = _mapper.Map<VoteUpdationCommand>(request);

        ErrorOr<RatingDto> rating = await _voteService.UpdateVoteAsync(command);

        return ReturnActionResult(rating);
    }

    private IActionResult ReturnActionResult(ErrorOr<RatingDto> rating)
    {
        return rating.Match(
            ratingDto => Ok(ratingDto),
            errors => Problem(errors));
    }
}