namespace podfy_catalog_application.Models;

public class CatalogResponseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public string FileKey { get; set; }
    public DateTime CreateAt { get; set; }
}
