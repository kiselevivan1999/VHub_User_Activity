namespace VHub.UserActivities.Domain.Entities.Reviews;

/// <summary>
/// Сущность рецензии.
/// </summary>
public class ReviewEntity
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
    public byte Type { get; set; }
    
    /// <summary>
    /// Дата создания.
    /// </summary>
    public DateTime CreatedAt { get; set; }
}