using podfy_catalog_application.Context;
using podfy_catalog_application.Entities;
using podfy_catalog_application.Models;
using Dapper;
using System.Text;

namespace podfy_catalog_application.Repository
{
    public class CatalogQueryRepository : ICatalogQueryRepository
    {
        private readonly ICatalogQueryContext _context;
        private readonly ILogger<CatalogQueryRepository> _logger;

        public CatalogQueryRepository(ICatalogQueryContext context, ILogger<CatalogQueryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Catalog> GetAsync(long id)
        {
            try
            {
                return await _context.CreateConnection().QueryFirstOrDefaultAsync<Catalog>($"SELECT * FROM Catalog WHERE Id = {id} LIMIT 1");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Catalog>> GetAllAsync()
        {
            try
            {
                return await _context.CreateConnection().QueryAsync<Catalog>("SELECT * FROM Catalog");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<Catalog>> GetAllWithFilter(CatalogFilterRequestDto catalogFilterRequest)
        {
            try
            {
                var query = new StringBuilder();
                query.AppendLine("SELECT * FROM Catalog ");

                if (!string.IsNullOrEmpty(catalogFilterRequest.Search))
                    query.AppendLine($"WHERE Title LIKE '%{catalogFilterRequest.Search}%' ");

                if (catalogFilterRequest.Skip.HasValue && catalogFilterRequest.Take.HasValue)
                    query.AppendLine($"LIMIT {catalogFilterRequest.Skip.Value}, {catalogFilterRequest.Take.Value} ");

                    return await _context.CreateConnection().QueryAsync<Catalog>(query.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
