using VHub.UserActivities.Api.Contracts.Enums;

namespace VHub.UserActivities.Api.Contracts.FavoriteOptions;

/// <summary>
/// Запрос на добавление любимого жанра пользователю.
/// </summary>
public class AddFavoriteGenreToUserRequest
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