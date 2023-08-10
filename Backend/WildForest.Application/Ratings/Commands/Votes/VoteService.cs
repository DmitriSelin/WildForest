using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Ratings.Common;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Enums;
using WildForest.Application.Common.Interfaces.Persistence.UnitOfWork;

namespace WildForest.Application.Ratings.Commands.Votes;

public sealed class VoteService : IVoteService
{
    private readonly IUnitOfWork _unitOfWork;

    public VoteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<RatingDto>> VoteAsync(VoteCreationCommand command)
    {
        var ratingId = RatingId.Create(command.RatingId);
        var userId = UserId.Create(command.UserId);

        Rating? rating = await _unitOfWork.RatingRepository.GetRatingByIdWithVotesByUserIdAsync(ratingId, userId);

        if (rating is null)
            return Errors.Rating.NotFoundById;

        bool isVoteExists = IsVoteExists(rating);

        if (isVoteExists)
            return Errors.Rating.DuplicateVote;

        var vote = rating.CreateVote(userId, (VoteResult)command.VoteResult);

        await _unitOfWork.SaveChangesAsync();

        return new RatingDto(rating.Id.Value, vote.Id.Value, command.VoteResult, rating.Points);
    }

    public async Task<ErrorOr<RatingDto>> UpdateVoteAsync(VoteUpdationCommand command)
    {
        var ratingId = RatingId.Create(command.RatingId);
        var userId = UserId.Create(command.UserId);
        var voteId = VoteId.Create(command.VoteId);

        var vote = await _unitOfWork.RatingRepository.GetVoteByIdAndUserIdWithRatingAsync(ratingId, userId, voteId);

        if (vote is null)
            return Errors.Rating.VoteNotFound;

        if (vote.Rating is null)
            return Errors.Rating.NotFoundById;

        vote.Rating.ChangeVote(vote);
        await _unitOfWork.SaveChangesAsync();

        return new RatingDto(vote.Rating.Id.Value, vote.Id.Value, (byte)vote.Result, vote.Rating.Points);
    }

    private static bool IsVoteExists(Rating rating)
    {
        if (rating.Votes is null || !rating.Votes.Any())
            return false;

        return true;
    }
}