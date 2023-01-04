using podfy_catalog_application.Mappings;
using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using MySqlConnector;
using System.Data;

namespace podfy_catalog_application.Context
{
    public class CatalogQueryContext : IDisposable, ICatalogQueryContext
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _connection;
        private readonly ISecretManagerContext _secretManagerContext;

        public CatalogQueryContext(IConfiguration configuration, ISecretManagerContext secretManagerContext)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _secretManagerContext = secretManagerContext ?? throw new ArgumentNullException(nameof(secretManagerContext));
            ModelCreating();
        }

        public IDbConnection CreateConnection()
        {
            var secretModel = _secretManagerContext.GetSecretValue(_configuration.GetSection("AWS:DbSecretManager").Value);
            var splitedHost = secretModel.Host.Split($"{secretModel.DbClusterIdentifier}.cluster");
            var readHost = $"{secretModel.DbClusterIdentifier}.cluster-ro{splitedHost[1]}";

            var connection = $"Server={readHost};Port={secretModel.Port};Database={secretModel.DbName};Uid={secretModel.UserName};Pwd={secretModel.Password}";

            _connection = new MySqlConnection(connection);
            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void ModelCreating()
        {
            if (FluentMapper.EntityMaps.Count == 0)
            {
                FluentMapper.Initialize(config =>
                {
                    config.AddMap(new CatalogMappingDAP());
                    config.ForDommel();
                });
            }
        }
    }
}
