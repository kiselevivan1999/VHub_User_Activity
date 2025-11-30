namespace VHub.UserActivities.Api.Contracts.Enums;

/// <summary>
/// Тип лайка.
/// </summary>
public enum ReviewLikeType
{
    /// <summary>
    /// Не определено.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Лайк.
    /// </summary>
    Like = 1,
    
    /// <summary>
    /// Дизлайк.
    /// </summary>
    Dislike = 2,
}