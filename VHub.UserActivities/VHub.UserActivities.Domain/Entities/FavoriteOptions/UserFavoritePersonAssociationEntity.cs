namespace VHub.UserActivities.Domain.Entities.FavoriteOptions;

/// <summary>
/// Сущность ассоциации пользователя с любимой персоной.
/// </summary>
public class UserFavoritePersonAssociationEntity
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