using podfy_catalog_application.Entities;

namespace podfy_catalog_application.Repository;

public interface ICatalogCommandoRepository
{
    Task CreateAsync(Catalog catalog);
    Task UpdateAsync(Catalog catalog);
    Task DeleteAsync(Catalog catalog);
}
