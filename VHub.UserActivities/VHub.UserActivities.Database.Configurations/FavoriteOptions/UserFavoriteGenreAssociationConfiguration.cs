using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.FavoriteOptions;

namespace VHub.UserActivities.Database.Configurations.FavoriteOptions;

public class UserFavoriteGenreAssociationConfiguration :  IEntityTypeConfiguration<UserFavoriteGenreAssociationEntity>
{
    public void Configure(EntityTypeBuilder<UserFavoriteGenreAssociationEntity> builder)
    {
        builder.ToTable(name: "user_favorite_genre_associations", schema: "user_activities");

        builder.HasKey(x =>
            new
            {
                x.UserId,
                x.Genre,
            });

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("user_id");

        builder.Property(x => x.Genre)
            .IsRequired()
            .HasColumnName("genre");
    }
}