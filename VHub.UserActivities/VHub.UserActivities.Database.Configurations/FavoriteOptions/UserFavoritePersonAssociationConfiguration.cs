using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.FavoriteOptions;

namespace VHub.UserActivities.Database.Configurations.FavoriteOptions;

public class UserFavoritePersonAssociationConfiguration : IEntityTypeConfiguration<UserFavoritePersonAssociationEntity>
{
    public void Configure(EntityTypeBuilder<UserFavoritePersonAssociationEntity> builder)
    {
        builder.ToTable(name: "user_favorite_person_associations", schema: "user_activities");

        builder.HasKey(x =>
            new
            {
                x.UserId,
                x.PersonId,
            });

        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("user_id");

        builder.Property(x => x.PersonId)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24)
            .HasColumnName("person_id");
    }
}