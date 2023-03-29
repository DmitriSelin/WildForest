using WildForest.Application.Marks.Common;
using ErrorOr;

namespace WildForest.Application.Marks.Commands.PutRating;

public interface IRatingCommandHandler
{
    Task<ErrorOr<MarkDto>> PutRatingAsync(RatingCommand command);
}