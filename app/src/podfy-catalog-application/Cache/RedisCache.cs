using podfy_catalog_application.Context;
using StackExchange.Redis;

namespace podfy_catalog_application.Cache
{
    public class RedisCache : IRedisCache
    {
        private IDatabase _database;
        private readonly ILogger<RedisCache> _logger;

        public RedisCache(IParameterStoreContext parameterStoreContext
            , ILogger<RedisCache> logger
            , IConfiguration configuration)
        {
            _logger = logger;
            var connection = parameterStoreContext.GetSecretValue(configuration.GetSection("AWS:CacheParameterStore").Value);
            SetDatabase(connection);
        }

        private void SetDatabase(string connection)
        {
            try
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connection);
                _database = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public bool StringSet(string key, string value)
        {
            try
            {
                return _database.StringSet(key, value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

        }

        public string StringGet(string key)
        {
            try
            {
                return _database.StringGet(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
