using Microsoft.EntityFrameworkCore;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Domain.Entities.FavoriteOptions;
using VHub.UserActivities.Domain.Entities.Reviews;

namespace VHub.UserActivities.Application.FavoriteOptions.Repositories;

internal class FavoriteOptionsRepository(UserActivitiesDbContext dbContext) : IFavoriteOptionsRepository
{
    private readonly UserActivitiesDbContext _dbContext =
        dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    
    public async Task CreateUserFavoriteGenreAssociationAsync(Guid userId, short genreType, CancellationToken cancellationToken)
    {
        var entity = new UserFavoriteGenreAssociationEntity
        {
            UserId = userId,
            Genre = genreType,
        };
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserFavoriteGenreAssociationAsync(Guid userId, short genreType, CancellationToken cancellationToken)
    {
        await _dbContext.UserFavoriteGenreAssociations
            .Where(x => x.UserId == userId && x.Genre == genreType)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task CreateUserFavoritePersonAssociationAsync(Guid userId, string personId, CancellationToken cancellationToken)
    {
        var entity = new UserFavoritePersonAssociationEntity
        {
            UserId = userId,
            PersonId = personId,
        };
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserFavoritePersonAssociationAsync(Guid userId, string personId, CancellationToken cancellationToken)
    {
        await _dbContext.UserFavoritePersonAssociations
            .Where(x => x.UserId == userId && x.PersonId == personId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        short[] favoriteGenreTypes, string[] favoritePersonIds, CancellationToken cancellationToken)
    {
        // todo Оптимизировать и добавить проверки на null и Length == 0.
        var usersWithFavoriteGenres = _dbContext.UserFavoriteGenreAssociations
            .Where(x => favoriteGenreTypes == null || favoriteGenreTypes.Length == 0 || favoriteGenreTypes.Contains(x.Genre))
            .Select(x => x.UserId);
        
        var usersWithFavoritePersons = _dbContext.UserFavoritePersonAssociations
            .Where(x => favoritePersonIds == null || favoritePersonIds.Length == 0 || favoritePersonIds.Contains(x.PersonId))
            .Select(x => x.UserId);

        return await usersWithFavoriteGenres
            .Union(usersWithFavoritePersons)
            .Distinct()
            .ToArrayAsync(cancellationToken);
    }

    public async Task WriteNotifyMessage(Guid[] userIds, string movieTitle)
    {
        var entities = userIds.Select(userId => new ReviewEntity
        {
            AuthorId = userId,
            MovieId = movieTitle,
            Content = $"Уважаемый/ая {userId}, вышла новинка \"{movieTitle}\"! Скорее смотрите!",
            CreatedAt = DateTime.Now,
        });
        
        await _dbContext.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }
}