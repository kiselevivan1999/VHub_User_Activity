using VHub.UserActivities.Api.Contracts.Enums;

namespace VHub.UserActivities.Api.Contracts.Reviews;

/// <summary>
/// Запрос на лайк рецензии.
/// </summary>
public class LikeReviewRequest
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