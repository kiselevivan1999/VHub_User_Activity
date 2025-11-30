namespace VHub.UserActivities.Application.Contracts.Catalogs;

/// <summary>
/// Сущность каталога.
/// </summary>
public class CatalogDto
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