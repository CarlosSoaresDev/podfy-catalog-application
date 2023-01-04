using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;

namespace podfy_catalog_application.Repository
{
    public interface ICatalogQueryRepository
    {
        Task<IEnumerable<Catalog>> GetAllAsync();
        Task<Catalog> GetAsync(long id);
        Task<IEnumerable<Catalog>> GetAllWithFilter(CatalogFilterRequestDto catalogFilterRequest);
    }
}
