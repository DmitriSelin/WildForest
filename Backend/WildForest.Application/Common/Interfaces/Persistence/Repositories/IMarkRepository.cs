using WildForest.Domain.Marks;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.ValueObjects;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IMarkRepository
{
    Task AddMarkAsync(Mark mark);

    Task<Mark?> GetMarkByIdWithVotesByUserIdAsync(MarkId markId, UserId userId);

    Task SaveChangesAsync();
}