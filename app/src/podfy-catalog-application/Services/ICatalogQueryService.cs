using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;

namespace podfy_catalog_application.Services
{
    public interface ICatalogQueryService
    {        
        Task<IEnumerable<CatalogResponseDto>> GetAllAsync();
        Task<CatalogResponseDto> GetAsync(long id);
        Task<IEnumerable<CatalogResponseDto>> GetAllWithFilter(CatalogFilterRequestDto catalogFilterRequest);        
    }
}
