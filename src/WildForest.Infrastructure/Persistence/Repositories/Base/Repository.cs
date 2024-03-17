using WildForest.Application.Common.Interfaces.Persistence.Base;
using WildForest.Domain.Common.Models;
using WildForest.Infrastructure.Persistence.Context;

namespace WildForest.Infrastructure.Persistence.Repositories.Base;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly WildForestDbContext _context;

    public Repository(WildForestDbContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken token = default)
    {
        await _context.AddAsync(entity, token);
    }

    public virtual void Update(TEntity entity)
    {
        _context.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        _context.Remove(entity);
    }
}