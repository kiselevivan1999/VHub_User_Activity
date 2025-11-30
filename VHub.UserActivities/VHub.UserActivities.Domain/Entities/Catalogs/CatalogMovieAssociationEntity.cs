namespace VHub.UserActivities.Domain.Entities.Catalogs;

/// <summary>
/// Сущность ассоциации каталога с фильмом.
/// </summary>
public class CatalogMovieAssociationEntity
{
    /// <summary>
    /// ID каталога.
    /// </summary>
    public long CatalogId { get; set; }
    
    /// <summary>
    /// ID фильма.
    /// </summary>
    public string MovieId { get; set; }
}