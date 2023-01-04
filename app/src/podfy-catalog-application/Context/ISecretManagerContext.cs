namespace podfy_catalog_application.Context
{
    public interface ISecretManagerContext
    {
        SecretManagerModel GetSecretValue(string secretName);
    }
}
