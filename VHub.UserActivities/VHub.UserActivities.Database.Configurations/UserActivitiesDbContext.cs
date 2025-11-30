using System.Reflection;
using Microsoft.EntityFrameworkCore;
using VHub.UserActivities.Domain.Entities.Catalogs;
using VHub.UserActivities.Domain.Entities.FavoriteOptions;
using VHub.UserActivities.Domain.Entities.MovieRates;
using VHub.UserActivities.Domain.Entities.Reviews;

namespace VHub.UserActivities.Database.Configurations;

public class UserActivitiesDbContext(DbContextOptions<UserActivitiesDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Каталоги.
    /// </summary>
    public DbSet<CatalogEntity> Catalogs { get; set; }
    
    /// <summary>
    /// Ассоциации каталога с фильмом.
    /// </summary>
    public DbSet<CatalogMovieAssociationEntity> CatalogMovieAssociations { get; set; }
    
    /// <summary>
    /// Оценки фильмов.
    /// </summary>
    public DbSet<MovieRateEntity> MovieRates { get; set; }
    
    /// <summary>
    /// Рецензии.
    /// </summary>
    public DbSet<ReviewEntity> Reviews { get; set; }
    
    /// <summary>
    /// Лайки рецензий.
    /// </summary>
    public DbSet<ReviewLikeEntity> ReviewLikes { get; set; }
    
    /// <summary>
    /// Ассоциации пользователей с любимыми жанрами.
    /// </summary>
    public DbSet<UserFavoriteGenreAssociationEntity> UserFavoriteGenreAssociations { get; set; }
    
    /// <summary>
    /// Ассоциации пользователей с любимыми персонами.
    /// </summary>
    public DbSet<UserFavoritePersonAssociationEntity> UserFavoritePersonAssociations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}