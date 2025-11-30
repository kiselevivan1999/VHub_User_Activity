namespace VHub.UserActivities.Common.Enums;

/// <summary>
/// Тип рецензии.
/// </summary>
public enum ReviewType
{
    /// <summary>
    /// Не определено.
    /// </summary>
    Unknown = 0,
    
    /// <summary>
    /// Позитивная.
    /// </summary>
    Positive = 1,
    
    /// <summary>
    /// Негативная.
    /// </summary>
    Negative = 2,
    
    /// <summary>
    /// Смешанная.
    /// </summary>
    Mixed = 3,
}