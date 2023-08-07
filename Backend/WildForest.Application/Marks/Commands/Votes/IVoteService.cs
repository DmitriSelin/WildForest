using ErrorOr;
using WildForest.Application.Marks.Common;

namespace WildForest.Application.Marks.Commands.Votes;

public interface IVoteService
{
    Task<ErrorOr<MarkDto>> VoteAsync(VoteCreationCommand command);
}