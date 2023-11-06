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

    [HttpPost("vote/up")]
    public async Task<IActionResult> UpVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateUpVote(request.RatingId, request.UserId);

        ErrorOr<RatingDto> rating = await _voteService.VoteAsync(command);

        return ReturnActionResult(rating);
    }

    [HttpPost("vote/down")]
    public async Task<IActionResult> DownVote(VoteCreationRequest request)
    {
        var command = VoteCreationCommand.CreateDownVote(request.RatingId, request.UserId);

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