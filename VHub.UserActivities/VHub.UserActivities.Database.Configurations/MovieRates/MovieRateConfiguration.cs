using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VHub.UserActivities.Domain.Entities.MovieRates;

namespace VHub.UserActivities.Database.Configurations.MovieRates;

public class MovieRateConfiguration : IEntityTypeConfiguration<MovieRateEntity>
{
    public void Configure(EntityTypeBuilder<MovieRateEntity> builder)
    {
        builder.ToTable(name: "movie_rates", schema: "user_activities");

        builder.HasKey(x =>
            new
            {
                x.AppraiserId,
                x.MovieId,
            });

        builder.Property(x => x.AppraiserId)
            .IsRequired()
            .HasColumnType("uuid")
            .HasColumnName("appraiser_id");

        builder.Property(x => x.MovieId)
            .IsRequired()
            .IsUnicode()
            .HasMaxLength(24)
            .HasColumnName("movie_id");
        
        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnName("value");
    }
}