using ErrorOr;
using WildForest.Domain.Common.Errors;
using WildForest.Application.Common.Interfaces.Persistence.Repositories;
using WildForest.Application.Marks.Common;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Marks;
using WildForest.Domain.Marks.Enums;

namespace WildForest.Application.Marks.Commands.Votes;

public sealed class VoteService : IVoteService
{
    private readonly IMarkRepository _markRepository;

    public VoteService(IMarkRepository markRepository)
    {
        _markRepository = markRepository;
    }

    public async Task<ErrorOr<MarkDto>> VoteAsync(VoteCreationCommand command)
    {
        var markId = MarkId.Create(command.MarkId);
        var userId = UserId.Create(command.UserId);

        Mark? mark = await _markRepository.GetMarkByIdWithVotesByUserIdAsync(markId, userId);

        if (mark is null)
            return Errors.Mark.NotFoundById;

        bool isVoteExists = IsVoteExists(mark);

        if (isVoteExists)
            return Errors.Mark.DuplicateVote;

        var vote = mark.ChangeRating(userId, (VoteResult)command.VoteResult);

        await _markRepository.SaveChangesAsync();

        return new MarkDto(mark.Id.Value, vote.Id.Value, command.VoteResult, mark.Points);
    }

    private static bool IsVoteExists(Mark mark)
    {
        if (mark.Votes is null || !mark.Votes.Any())
            return false;

        return true;
    }
}