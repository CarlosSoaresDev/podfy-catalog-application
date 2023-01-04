using podfy_catalog_application.Context;
using podfy_catalog_application.Entities;
using System.Diagnostics.CodeAnalysis;

namespace podfy_catalog_application.Repository.Base;

[ExcludeFromCodeCoverage]
public abstract class CommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : EntityBase, new()
{
    private readonly ICatalogCommandContext _context;

    protected CommandRepository(ICatalogCommandContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync(TEntity entity)
    {
        if (entity.Id > 0)
            _context.ChangeEntityState(entity);
        else
        {
            var dbSet = _context.CreateSet<TEntity>();
            dbSet.Add(entity);
        }
        return await _context.SaveContextChangesAsync();
    }

    public void Save(TEntity entity)
    {
        if (entity.Id > 0)
            _context.ChangeEntityState(entity);
        else
        {
            var dbSet = _context.CreateSet<TEntity>();
            dbSet.Add(entity);
        }
        _context.SaveContextChanges();
    }

    public void Remove(TEntity entity)
    {
        var dbSet = _context.CreateSet<TEntity>();
        dbSet.Remove(entity);
        _context.SaveContextChanges();
    }

    public Task RemoveAsync(TEntity entity)
    {
        var dbSet = _context.CreateSet<TEntity>();
        dbSet.Remove(entity);

        return _context.SaveContextChangesAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
