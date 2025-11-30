using VHub.UserActivities.Api.Contracts.Enums;

namespace VHub.UserActivities.Api.Contracts.Reviews;

/// <summary>
/// Запрос на создание рецензии.
/// </summary>
public class CreateReviewRequest
{
    /// <summary>
    /// ID рецензента.
    /// </summary>
    public Guid AuthorId { get; set; }
    
    /// <summary>
    /// ID фильма, на который написана рецензия.
    /// </summary>
    public string MovieId { get; set; }
    
    /// <summary>
    /// Содержимое.
    /// </summary>
    public string Content { get; set; }
    
    /// <summary>
    /// Тип рецензии.
    /// </summary>
    public ReviewType Type { get; set; }
}