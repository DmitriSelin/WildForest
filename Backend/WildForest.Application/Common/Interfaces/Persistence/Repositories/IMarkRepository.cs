using WildForest.Domain.Marks.Entities;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories;

public interface IMarkRepository
{
    Task AddMarkAsync(Mark mark);
}