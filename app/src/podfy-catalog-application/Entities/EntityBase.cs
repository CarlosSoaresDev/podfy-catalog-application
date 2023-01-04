namespace podfy_catalog_application.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
