using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.Catalogs;

namespace VHub.UserActivities.Database.Configurations.Catalogs;

public class CatalogConfiguration : IEntityTypeConfiguration<CatalogEntity>
{
    public void Configure(EntityTypeBuilder<CatalogEntity> builder)
    {
        builder.ToTable(name: "catalogs", schema: "user_activities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("id");

        builder.Property(x => x.Title)
            .HasMaxLength(30)
            .IsUnicode()
            .IsRequired()
            .HasColumnName("title");

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("user_id");
    }
}