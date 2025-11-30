namespace VHub.UserActivities.Domain.Entities.Catalogs;

/// <summary>
/// Сущность каталога.
/// </summary>
public class CatalogEntity
{
    /// <summary>
    /// ID каталога.
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// ID пользователя, создавшего каталог.
    /// </summary>
    public Guid UserId { get; set; }
}