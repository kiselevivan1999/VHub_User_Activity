namespace VHub.UserActivities.Application.Contracts.Catalogs;

/// <summary>
/// Сущность ассоциации каталога с фильмом.
/// </summary>
public class CatalogMovieAssociationDto
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