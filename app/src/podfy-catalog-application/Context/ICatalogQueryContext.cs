using System.Data;

namespace podfy_catalog_application.Context
{
    public interface ICatalogQueryContext
    {
        IDbConnection CreateConnection();
    }
}
