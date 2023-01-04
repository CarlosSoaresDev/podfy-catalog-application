namespace podfy_catalog_application.Entities
{
    public class Catalog : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string FileKey { get; set; }
    }
}
