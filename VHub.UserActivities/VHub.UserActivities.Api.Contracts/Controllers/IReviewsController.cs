using RestEase;
using VHub.UserActivities.Api.Contracts.Reviews;

namespace VHub.UserActivities.Api.Contracts.Controllers;

[BasePath("user-activities/reviews")]
public interface IReviewsController
{
    /// <summary>
    /// Создаёт рецензию.
    /// </summary>
    /// <param name="request">Запрос на создание рецензии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ID созданной рецензии.</returns>
    [Post("new")]
    Task<long> CreateReviewAsync([Body] CreateReviewRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет рецензию.
    /// </summary>
    /// <param name="id">ID рецензии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("delete/{id:long}")]
    Task DeleteReviewAsync([Path] long id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ставит лайк/дизлайк на рецензию.
    /// </summary>
    /// <param name="request">Запрос на лайк рецензии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Post("like")]
    Task LikeReviewAsync([Body] LikeReviewRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Отменяет лайк/дизлайк на рецензию.
    /// </summary>
    /// <param name="appraiserId">ID оценщика.</param>
    /// <param name="reviewId">ID рецензии.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("like/delete")]
    Task DeleteReviewLikeAsync(
        [Query] Guid appraiserId, [Query] long reviewId, CancellationToken cancellationToken = default);
}