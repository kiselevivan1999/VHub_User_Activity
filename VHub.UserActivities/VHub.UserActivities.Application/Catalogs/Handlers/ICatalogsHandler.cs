using VHub.UserActivities.Application.Contracts.Catalogs;

namespace VHub.UserActivities.Application.Catalogs.Handlers;

public interface ICatalogsHandler
{
    Task<long> CreateCatalogAsync(CatalogDto catalog, CancellationToken cancellationToken);
    
    Task DeleteCatalogAsync(long id, CancellationToken cancellationToken);
    
    Task CreateCatalogMovieAssociationAsync(CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken);
    
    Task<int> DeleteCatalogMovieAssociationAsync(CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken);
    
    Task<int> DeleteCatalogMovieAssociationsByCatalogIdAsync(long catalogId, CancellationToken cancellationToken);
}