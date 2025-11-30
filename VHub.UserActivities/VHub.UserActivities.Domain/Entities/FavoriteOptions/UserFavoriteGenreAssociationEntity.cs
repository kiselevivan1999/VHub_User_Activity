namespace VHub.UserActivities.Domain.Entities.FavoriteOptions;

/// <summary>
/// Сущность ассоциации пользователя с любимым жанром.
/// </summary>
public class UserFavoriteGenreAssociationEntity
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Жанр.
    /// </summary>
    public short Genre { get; set; }
}