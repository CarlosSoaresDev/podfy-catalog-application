namespace podfy_catalog_application.Cache
{
    public interface IRedisCache
    {
        public bool StringSet(string key, string value);

        public string StringGet(string key);
    }
}
