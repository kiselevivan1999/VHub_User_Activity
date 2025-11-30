namespace VHub.UserActivities.Api.Contracts.Catalogs;

/// <summary>
/// Запрос на создание каталога.
/// </summary>
public class CreateCatalogRequest
{
    /// <summary>
    /// Название.
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// ID пользователя, создавшего каталог.
    /// </summary>
    public Guid UserId { get; set; }
}