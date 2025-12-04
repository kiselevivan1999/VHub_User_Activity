using Mapster;
using VHub.UserActivities.Application.FavoriteOptions.Repositories;
using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.FavoriteOptions.Handlers;

internal class FavoriteOptionsHandler(IFavoriteOptionsRepository repository) : IFavoriteOptionsHandler
{
    private readonly IFavoriteOptionsRepository _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task AddFavoriteGenreToUserAsync(Guid userId, GenreType genreType, CancellationToken cancellationToken) =>
        await _repository.CreateUserFavoriteGenreAssociationAsync(userId, (short)genreType, cancellationToken);

    public async Task DeleteFavoriteGenreFromUserAsync(
        Guid userId, GenreType genreType, CancellationToken cancellationToken) =>
        await _repository.DeleteUserFavoriteGenreAssociationAsync(userId, (short)genreType, cancellationToken);

    public async Task AddFavoritePersonToUserAsync(Guid userId, string personId, CancellationToken cancellationToken) =>
        await _repository.CreateUserFavoritePersonAssociationAsync(userId, personId, cancellationToken);

    public async Task DeleteFavoritePersonFromUserAsync(Guid userId, string personId,
        CancellationToken cancellationToken) =>
        await _repository.DeleteUserFavoritePersonAssociationAsync(userId, personId, cancellationToken);

    public async Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        GenreType[] favoriteGenreTypes, string[] favoritePersonIds, CancellationToken cancellationToken) =>
        await _repository.GetUserIdsByFavoriteOptionsAsync(
            favoriteGenreTypes.Adapt<short[]>(), favoritePersonIds, cancellationToken);

    // todo Удалить (тестовый метод)
    public async Task WriteNotifyMessage(string[] users, string title)
    {
        await _repository.WriteNotifyMessage(users, title);
    }
}