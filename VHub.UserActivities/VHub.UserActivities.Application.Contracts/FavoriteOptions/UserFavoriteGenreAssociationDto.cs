using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.Contracts.FavoriteOptions;

/// <summary>
/// Ассоциация пользователя с любимым жанром.
/// </summary>
public class UserFavoriteGenreAssociationDto
{
    /// <summary>
    /// ID пользователя.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Жанр.
    /// </summary>
    public GenreType GenreType { get; set; }
}