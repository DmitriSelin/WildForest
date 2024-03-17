using ErrorOr;
using WildForest.Application.Ratings.Common;

namespace WildForest.Application.Ratings.Commands.Votes;

public interface IVoteService
{
    Task<ErrorOr<RatingDto>> VoteAsync(VoteCreationCommand command);

    Task<ErrorOr<RatingDto>> UpdateVoteAsync(VoteUpdationCommand command);

    Task<ErrorOr<RatingDto>> GetVoteAsync(Guid ratingId, Guid userId);
}