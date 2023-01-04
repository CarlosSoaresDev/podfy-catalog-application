using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace podfy_catalog_application.Context;

[ExcludeFromCodeCoverage]
public class CatalogCommandContext : DbContext, ICatalogCommandContext
{
    public CatalogCommandContext(DbContextOptions<CatalogCommandContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }

    public int SaveContextChanges()
    {
        return SaveChanges();
    }

    public Task<int> SaveContextChangesAsync()
    {
        return SaveChangesAsync();
    }

    public void ChangeEntityState(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }
}


