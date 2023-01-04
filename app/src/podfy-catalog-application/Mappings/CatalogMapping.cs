using podfy_catalog_application.Entities;
using Dapper.FluentMap.Dommel.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace podfy_catalog_application.Mappings;
public class CatalogMappingEF : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).HasMaxLength(255).IsRequired();
        builder.Property(c => c.Description);
        builder.Property(c => c.UserId).HasMaxLength(255).IsRequired(); 
        builder.Property(c => c.FileKey).IsRequired();
        builder.Property(c => c.CreateAt).IsRequired();
    }
}

public class CatalogMappingDAP : DommelEntityMap<Catalog>
{
    public CatalogMappingDAP()
    {
        ToTable("Catalog");
        Map(x => x.Id).ToColumn("Id").IsKey();
        Map(x => x.Title).ToColumn("Title");
        Map(x => x.Description).ToColumn("Description");
        Map(x => x.UserId).ToColumn("UserId");
        Map(x => x.FileKey).ToColumn("FileKey");
        Map(x => x.CreateAt).ToColumn("CreateAt");
    }
}