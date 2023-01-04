using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Text.Json;

namespace podfy_catalog_application.Context
{
    public class SecretManagerContext : ISecretManagerContext
    {
        private const string CACHE_KEY_SECRETS = "CacheSecretsModel";
        private AmazonSecretsManagerClient Context { get; set; }
        private readonly IDistributedCache _cache;

        public SecretManagerContext(IDistributedCache cache)
        {
            _cache = cache;
            if (Debugger.IsAttached)
                Context = new AmazonSecretsManagerClient(Amazon.RegionEndpoint.USEast1);
            else
                Context = new AmazonSecretsManagerClient(Environment.GetEnvironmentVariable("ACCESS_KEY"), Environment.GetEnvironmentVariable("SECRET_KEY"), Amazon.RegionEndpoint.USEast1);

        }

        public SecretManagerModel GetSecretValue(string secretName)
        {
            var cache = _cache.GetString(CACHE_KEY_SECRETS);

            if (string.IsNullOrEmpty(cache))
            {
                GetSecretValueRequest request = new GetSecretValueRequest()
                {
                    SecretId = secretName
                };

                cache = (Context.GetSecretValueAsync(request).GetAwaiter().GetResult()).SecretString;
                _cache.SetString(CACHE_KEY_SECRETS, cache);
            }

            return JsonSerializer.Deserialize<SecretManagerModel>(cache, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public class SecretManagerModel
    {
        public string DbClusterIdentifier { get; set; }
        public string Password { get; set; }
        public string DbName { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
    }
}
