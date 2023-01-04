using Microsoft.EntityFrameworkCore;

namespace podfy_catalog_application.Context;

public interface ICatalogCommandContext
{
    DbSet<T> CreateSet<T>() where T : class;
    int SaveContextChanges();
    Task<int> SaveContextChangesAsync();
    void ChangeEntityState(object entity);
}
