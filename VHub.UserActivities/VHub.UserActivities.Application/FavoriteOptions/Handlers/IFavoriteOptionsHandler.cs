using VHub.UserActivities.Common.Enums;

namespace VHub.UserActivities.Application.FavoriteOptions.Handlers;

public interface IFavoriteOptionsHandler
{
    Task AddFavoriteGenreToUserAsync(Guid userId, GenreType genreType, CancellationToken cancellationToken);

    Task DeleteFavoriteGenreFromUserAsync(Guid userId, GenreType genreType, CancellationToken cancellationToken);

    Task AddFavoritePersonToUserAsync(Guid userId, string personId, CancellationToken cancellationToken);

    Task DeleteFavoritePersonFromUserAsync(Guid userId, string personId, CancellationToken cancellationToken);

    Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        GenreType[] favoriteGenreTypes, string[] favoritePersonIds, CancellationToken cancellationToken);

    Task WriteNotifyMessage(string[] userIds, string str);
}