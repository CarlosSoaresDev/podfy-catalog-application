using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Text.Json;

namespace podfy_catalog_application.Context
{
    public class ParameterStoreContext : IParameterStoreContext
    {
        private const string CACHE_KEY_SECRETS = "ParameterStoreModel";
        private AmazonSimpleSystemsManagementClient Context { get; set; }
        private readonly IDistributedCache _cache;

        public ParameterStoreContext(IDistributedCache cache)
        {
            _cache = cache;
            if (Debugger.IsAttached)
                Context = new AmazonSimpleSystemsManagementClient(Amazon.RegionEndpoint.USEast1);
            else
                Context = new AmazonSimpleSystemsManagementClient(Environment.GetEnvironmentVariable("ACCESS_KEY"), Environment.GetEnvironmentVariable("SECRET_KEY"), Amazon.RegionEndpoint.USEast1);

        }

        public string GetSecretValue(string parameterName)
        {
            var cache = _cache.GetString(CACHE_KEY_SECRETS);

            if (string.IsNullOrEmpty(cache))
            {
                var request = new GetParameterRequest()
                {
                    Name = parameterName
                };

                cache = (Context.GetParameterAsync(request).GetAwaiter().GetResult()).Parameter.Value;
                _cache.SetString(CACHE_KEY_SECRETS, cache);
            }

            return cache;
        }
    }
}
