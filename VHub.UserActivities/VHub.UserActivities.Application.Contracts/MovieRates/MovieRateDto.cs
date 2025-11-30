namespace VHub.UserActivities.Application.Contracts.MovieRates;

/// <summary>
/// Сущность оценки фильма.
/// </summary>
public class MovieRateDto
{
    /// <summary>
    /// ID оценщика.
    /// </summary>
    public Guid AppraiserId { get; set; }
    
    /// <summary>
    /// ID фильма.
    /// </summary>
    public string MovieId { get; set; }
    
    /// <summary>
    /// Значение оценки (от 1 до 10).
    /// </summary>
    public byte Value { get; set; }
}