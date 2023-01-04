namespace podfy_catalog_application.Models;

public class CatalogFilterRequestDto
{
    public string Search { get; private set; }
    public int? Skip { get; private set; }
    public int? Take { get; private set; }


    public CatalogFilterRequestDto(string search, int? skip, int? take)
    {
        Search = search;
        Skip = skip;    
        Take = take;
    }
}
