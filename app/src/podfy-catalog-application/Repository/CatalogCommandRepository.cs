using podfy_catalog_application.Context;
using podfy_catalog_application.Entities;
using podfy_catalog_application.Repository.Base;

namespace podfy_catalog_application.Repository;

public class CatalogCommandRepository : CommandRepository<Catalog>, ICatalogCommandoRepository
{
    private readonly ILogger<CatalogCommandRepository> _logger;

    public CatalogCommandRepository(ICatalogCommandContext context, ILogger<CatalogCommandRepository> logger) : base(context)
    {
        _logger = logger;
    }

    public async Task CreateAsync(Catalog catalog)
    {
        try
        {
            await SaveAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }       
    }

    public async Task UpdateAsync(Catalog catalog)
    {
        try
        {
            await SaveAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task DeleteAsync(Catalog catalog)
    {

        try
        {
            await RemoveAsync(catalog);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
