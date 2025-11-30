using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.Contracts.Reviews;

/// <summary>
/// Сущность рецензии.
/// </summary>
public class ReviewDto
{
    /// <summary>
    /// ID рецензии.
    /// </summary>
    public long Id { get; set; }
    
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
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}