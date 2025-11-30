using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.Contracts.Reviews;

/// <summary>
/// Сущность лайка рецензии.
/// </summary>
public class ReviewLikeDto
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
    public ReviewLikeType Type { get; set; }
}