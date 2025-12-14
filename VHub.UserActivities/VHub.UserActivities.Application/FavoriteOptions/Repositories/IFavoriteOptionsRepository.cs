namespace VHub.UserActivities.Application.FavoriteOptions.Repositories;

public interface IFavoriteOptionsRepository
{
    Task CreateUserFavoriteGenreAssociationAsync(Guid userId, short genreType, CancellationToken cancellationToken);

    Task DeleteUserFavoriteGenreAssociationAsync(Guid userId, short genreType, CancellationToken cancellationToken);

    Task CreateUserFavoritePersonAssociationAsync(Guid userId, string personId, CancellationToken cancellationToken);

    Task DeleteUserFavoritePersonAssociationAsync(Guid userId, string personId, CancellationToken cancellationToken);

    Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        short[] favoriteGenreTypes, string[] favoritePersonIds, CancellationToken cancellationToken);
}