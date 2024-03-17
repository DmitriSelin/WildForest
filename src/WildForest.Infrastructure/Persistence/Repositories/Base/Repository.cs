using WildForest.Application.Common.Interfaces.Persistence.Base;
using WildForest.Domain.Common.Models;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly WildForestDbContext Context;

    public Repository(WildForestDbContext context)
    {
        Context = context;
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await Context.AddAsync(entity, token);
    }

    public virtual void Update(TEntity entity)
    {
        Context.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        Context.Remove(entity);
    }
}