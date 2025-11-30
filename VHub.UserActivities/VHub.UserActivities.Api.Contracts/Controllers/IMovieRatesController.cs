using RestEase;
using VHub.UserActivities.Api.Contracts.MovieRates;

namespace VHub.UserActivities.Api.Contracts.Controllers;

[BasePath("user-activities/movie-rates")]
public interface IMovieRatesController
{
    /// <summary>
    /// Оценивает фильм.
    /// </summary>
    /// <param name="request">Запрос на создание оценки фильма.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Post("new")]
    Task CreateMovieRateAsync([Body] CreateMovieRateRequest request, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Отменяет оценку фильма.
    /// </summary>
    /// <param name="appraiserId">ID оценщика.</param>
    /// <param name="movieId">ID фильма.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("delete")]
    Task DeleteMovieRateAsync([Query] Guid appraiserId, [Query] string movieId, CancellationToken cancellationToken = default);
}