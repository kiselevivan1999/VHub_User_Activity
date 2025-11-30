namespace VHub.UserActivities.Api.Contracts.MovieRates;

/// <summary>
/// Запрос на создание оценки фильма.
/// </summary>
public class CreateMovieRateRequest
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