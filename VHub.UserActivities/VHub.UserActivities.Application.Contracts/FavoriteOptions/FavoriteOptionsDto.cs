using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.Contracts.FavoriteOptions;

/// <summary>
/// Опции избранного.
/// </summary>
public class FavoriteOptionsDto
{
    /// <summary>
    /// Любимые жанры.
    /// </summary>
    public short[] FavoriteGenreTypes { get; set; }
    
    /// <summary>
    /// Любимые персоны.
    /// </summary>
    public string[] FavoritePersonIds { get; set; }
}