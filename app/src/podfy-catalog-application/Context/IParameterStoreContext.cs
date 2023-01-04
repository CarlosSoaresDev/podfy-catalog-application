namespace podfy_catalog_application.Context
{
    public interface IParameterStoreContext
    {
        string GetSecretValue(string parameterName);
    }
}
