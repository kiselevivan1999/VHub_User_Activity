using Mapster;
using Microsoft.EntityFrameworkCore;
using VHub.UserActivities.Application.Contracts.Catalogs;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Domain.Entities.Catalogs;

namespace VHub.UserActivities.Application.Catalogs.Repositories;

internal class CatalogsRepository(UserActivitiesDbContext dbContext) : ICatalogsRepository
{
    private readonly UserActivitiesDbContext _dbContext =
        dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<long> CreateCatalogAsync(CatalogDto catalog, CancellationToken cancellationToken)
    {
        var entity = catalog.Adapt<CatalogEntity>();
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
    
    public async Task DeleteCatalogAsync(long id, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        await DeleteCatalogMovieAssociationsByCatalogIdAsync(id, cancellationToken);
        await _dbContext.Catalogs
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);
        
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task CreateCatalogMovieAssociationAsync(CatalogMovieAssociationDto catalogMovieAssociation,
        CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(catalogMovieAssociation.Adapt<CatalogMovieAssociationEntity>(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteCatalogMovieAssociationAsync(
        CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken) =>
        await _dbContext.CatalogMovieAssociations
            .Where(x => x.CatalogId == catalogMovieAssociation.CatalogId && x.MovieId == catalogMovieAssociation.MovieId)
            .ExecuteDeleteAsync(cancellationToken);

    public async Task<int> DeleteCatalogMovieAssociationsByCatalogIdAsync(long catalogId, CancellationToken cancellationToken) =>
        await _dbContext.CatalogMovieAssociations
            .Where(x => x.CatalogId == catalogId)
            .ExecuteDeleteAsync(cancellationToken);
}