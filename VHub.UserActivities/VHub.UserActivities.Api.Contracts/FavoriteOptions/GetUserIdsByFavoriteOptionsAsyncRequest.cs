using System.ComponentModel.DataAnnotations;
using VHub.UserActivities.Api.Contracts.Enums;

namespace VHub.UserActivities.Api.Contracts.FavoriteOptions;

/// <summary>
/// Запрос на получение списка IDs пользователей по опциям избранного.
/// </summary>
public class GetUserIdsByFavoriteOptionsAsyncRequest
{
    /// <summary>
    /// Любимые жанры.
    /// </summary>
    [Required, MinLength(1)]
    public GenreType[]? FavoriteGenreTypes { get; set; }

    /// <summary>
    /// Любимые персоны.
    /// </summary>
    [Required, MinLength(1)]
    public string[]? FavoritePersonIds { get; set; }
}