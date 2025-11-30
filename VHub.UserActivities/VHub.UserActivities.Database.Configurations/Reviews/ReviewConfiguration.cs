using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.Reviews;

namespace VHub.UserActivities.Database.Configurations.Reviews;

public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
{
    public void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        builder.ToTable(name: "reviews", schema: "user_activities");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName("id");

        builder.Property(x => x.AuthorId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("author_id");

        builder.Property(x => x.MovieId)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24)
            .HasColumnName("movie_id");
        
        builder.Property(x => x.Content)
            .IsRequired()
            .IsUnicode()
            .HasColumnName("content");
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnName("type");
        
        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}