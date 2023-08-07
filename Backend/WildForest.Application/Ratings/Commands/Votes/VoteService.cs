using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Application.Ratings.Common;
using WildForest.Domain.Ratings.ValueObjects;
using WildForest.Domain.Ratings;
using WildForest.Domain.Ratings.Enums;

namespace WildForest.Application.Ratings.Commands.Votes;

public sealed class VoteService : IVoteService
{
    private readonly IRatingRepository _ratingRepository;

    public VoteService(IRatingRepository ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    public async Task<ErrorOr<RatingDto>> VoteAsync(VoteCreationCommand command)
    {
        var ratingId = RatingId.Create(command.RatingId);
        var userId = UserId.Create(command.UserId);

        Rating? rating = await _ratingRepository.GetRatingByIdWithVotesByUserIdAsync(ratingId, userId);

        if (rating is null)
            return Errors.Rating.NotFoundById;

        bool isVoteExists = IsVoteExists(rating);

        if (isVoteExists)
            return Errors.Rating.DuplicateVote;

        var vote = rating.ChangeRating(userId, (VoteResult)command.VoteResult);

        await _ratingRepository.SaveChangesAsync();

        return new RatingDto(rating.Id.Value, vote.Id.Value, command.VoteResult, rating.Points);
    }

    private static bool IsVoteExists(Rating rating)
    {
        if (rating.Votes is null || !rating.Votes.Any())
            return false;

        return true;
    }
}