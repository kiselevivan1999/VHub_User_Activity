namespace VHub.UserActivities.Api.Contracts.FavoriteOptions;

/// <summary>
/// Запрос на добавление любимой персоны пользователю.
/// </summary>
public class AddFavoritePersonToUserRequest
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// ID персоны.
    /// </summary>
    public string PersonId { get; set; }
}