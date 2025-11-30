using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.Catalogs;

namespace VHub.UserActivities.Database.Configurations.Catalogs;

public class CatalogMovieAssociationConfiguration : IEntityTypeConfiguration<CatalogMovieAssociationEntity>
{
    public void Configure(EntityTypeBuilder<CatalogMovieAssociationEntity> builder)
    {
        builder.ToTable(name: "catalog_movie_associations", schema: "user_activities");

        builder.HasKey(x =>
            new
            {
                x.CatalogId,
                x.MovieId,
            });

        builder.Property(x => x.CatalogId)
            .IsRequired()
            .HasColumnName("catalog_id");

        builder.Property(x => x.MovieId)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24)
            .HasColumnName("movie_id");
    }
}