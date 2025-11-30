using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.Reviews;

namespace VHub.UserActivities.Database.Configurations.Reviews;

public class ReviewLikeConfiguration : IEntityTypeConfiguration<ReviewLikeEntity>
{
    public void Configure(EntityTypeBuilder<ReviewLikeEntity> builder)
    {
        builder.ToTable(name: "review_likes", schema: "user_activities");

        builder.HasKey(x =>
            new
            {
                x.AppraiserId,
                x.ReviewId,
            });
        
        builder.Property(x => x.AppraiserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("appraiser_id");

        builder.Property(x => x.ReviewId)
            .IsRequired()
            .HasColumnName("review_id");

        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnName("type");
    }
}