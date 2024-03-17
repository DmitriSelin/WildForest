using WildForest.Domain.Common.Models;

namespace WildForest.Application.Common.Interfaces.Persistence.Repositories.Base;

public interface IRepository<TEntity>
    where TEntity: class, IEntity
{
    Task AddAsync(TEntity entity, CancellationToken token = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);
}