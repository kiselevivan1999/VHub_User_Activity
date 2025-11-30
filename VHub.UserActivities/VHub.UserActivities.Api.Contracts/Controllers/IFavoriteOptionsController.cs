using RestEase;
using VHub.UserActivities.Api.Contracts.Enums;
using VHub.UserActivities.Api.Contracts.FavoriteOptions;

namespace VHub.UserActivities.Api.Contracts.Controllers;

[BasePath("user-activities/favorite-options")]
public interface IFavoriteOptionsController
{
    /// <summary>
    /// Создаёт любимый жанр у пользователя.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Post("add-favorite-genre")]
    Task AddFavoriteGenreToUserAsync([Body] AddFavoriteGenreToUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет любимый жанр у пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="genreType">Жанр.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("delete-favorite-genre")]
    Task DeleteFavoriteGenreFromUserAsync([Query] Guid userId, [Query] GenreType genreType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт любимую персону у пользователя.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Post("add-favorite-person")]
    Task AddFavoritePersonToUserAsync([Body] AddFavoritePersonToUserRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет любимую персону у пользователя.
    /// </summary>
    /// <param name="userId">ID пользователя.</param>
    /// <param name="personId">ID персоны.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("delete-favorite-person")]
    Task DeleteFavoritePersonFromUserAsync([Query] Guid userId, [Query] string personId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список IDs пользователей по опциям избранного.
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список IDs пользователей.</returns>
    [Post("get-user-ids-by-favorite-options")]
    Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        [Body] GetUserIdsByFavoriteOptionsAsyncRequest request, CancellationToken cancellationToken = default);
}