using podfy_catalog_application.Context;
using Dapper;
using Microsoft.Extensions.Caching.Distributed;
using MySqlConnector;

namespace podfy_catalog_application.Migrate
{
    public class Migrate
    {
        public Migrate(IConfiguration configuration, ILogger logger, IDistributedCache cache)
        {
            ExcuteQuery(configuration, logger, cache);
        }

        public void ExcuteQuery(IConfiguration configuration, ILogger logger, IDistributedCache cache)
        {
            try
            {
                logger.LogInformation("[Migrate] => [ExcuteQuery] => Executando a query de migração");

                var secretModel = new SecretManagerContext(cache).GetSecretValue(configuration.GetSection("AWS:DbSecretManager").Value);

                var connectionString = $"Server={secretModel.Host};Port={secretModel.Port};Database={secretModel.DbName};Uid={secretModel.UserName};Pwd={secretModel.Password}";

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute(@"
                            CREATE TABLE Catalog (
                                Id BIGINT PRIMARY KEY AUTO_INCREMENT NOT NULL,
                                Title VARCHAR(255) NOT NULL,
                                Description Text,
                                UserId VARCHAR(255) NOT NULL,
                                FileKey Text NOT NULL,
                                CreateAt DATETIME NOT NULL
                            );");

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[Migrate] => [ExcuteQuery] => Executando a query de migração {ex.Message}");
            }
        }
    }
}
