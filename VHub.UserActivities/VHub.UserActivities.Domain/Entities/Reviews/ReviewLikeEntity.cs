namespace VHub.UserActivities.Domain.Entities.Reviews;

/// <summary>
/// Сущность лайка рецензии.
/// </summary>
public class ReviewLikeEntity
{
    /// <summary>
    /// ID оценщика.
    /// </summary>
    public Guid AppraiserId { get; set; }
    
    /// <summary>
    /// ID рецензии.
    /// </summary>
    public long ReviewId { get; set; }
    
    /// <summary>
    /// Тип лайка.
    /// </summary>
    public byte Type { get; set; }
}