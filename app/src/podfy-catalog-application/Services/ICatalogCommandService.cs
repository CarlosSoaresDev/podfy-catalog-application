using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;

namespace podfy_catalog_application.Services
{
    public interface ICatalogCommandService
    {
        Task UpdateAsync(long id, CatalogRequestDto catalogRequestDto);
        Task CreateAsync(CatalogRequestDto catalogRequestDto);
        Task DeleteAsync(long id);
        
    }
}
