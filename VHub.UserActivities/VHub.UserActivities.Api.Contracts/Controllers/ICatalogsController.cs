using RestEase;
using VHub.UserActivities.Api.Contracts.Catalogs;

namespace VHub.UserActivities.Api.Contracts.Controllers;

[BasePath("user-activities/catalogs")]
public interface ICatalogsController
{
    /// <summary>
    /// Создаёт каталог.
    /// </summary>
    /// <param name="catalog">Запрос на создание каталога.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>ID созданного каталога.</returns>
    [Post("new")]
    Task<long> CreateCatalogAsync([Body] CreateCatalogRequest catalog, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет каталог.
    /// </summary>
    /// <param name="catalogId">ID каталога.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Task.</returns>
    [Delete("delete/{catalogId:long}")]
    Task DeleteCatalogAsync([Path] long catalogId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет фильм в каталог.
    /// </summary>
    /// <param name="catalogMovieAssociation">Ассоциация каталога с фильмом.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns></returns>
    [Post("add-movie")]
    Task CreateCatalogMovieAssociationAsync(
        [Body] CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет фильм из каталога.
    /// </summary>
    /// <param name="catalogId">ID каталога.</param>
    /// <param name="movieId">ID фильма.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество удалённых записей.</returns>
    [Delete("{catalogId:long}/delete-movie/{movie-id}")]
    Task<int> DeleteCatalogMovieAssociationAsync(
        [Path] long catalogId, [Path] string movieId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Очищает каталог.
    /// </summary>
    /// <param name="catalogId">ID каталога.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Количество удалённых фильмов в каталоге.</returns>
    [Delete("clear/{catalogId:long}")]
    Task<int> DeleteCatalogMovieAssociationsByCatalogIdAsync(
        [Path] long catalogId, CancellationToken cancellationToken = default);
}