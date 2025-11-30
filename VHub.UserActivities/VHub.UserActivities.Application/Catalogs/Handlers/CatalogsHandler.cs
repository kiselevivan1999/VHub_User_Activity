using VHub.UserActivities.Application.Catalogs.Repositories;
using VHub.UserActivities.Application.Contracts.Catalogs;

namespace VHub.UserActivities.Application.Catalogs.Handlers;

internal class CatalogsHandler(ICatalogsRepository repository) : ICatalogsHandler
{
    private readonly ICatalogsRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public Task<long> CreateCatalogAsync(CatalogDto catalog, CancellationToken cancellationToken) =>
        _repository.CreateCatalogAsync(catalog, cancellationToken);

    public Task DeleteCatalogAsync(long id, CancellationToken cancellationToken) =>
        _repository.DeleteCatalogAsync(id, cancellationToken);

    public Task CreateCatalogMovieAssociationAsync(
        CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken) =>
        _repository.CreateCatalogMovieAssociationAsync(catalogMovieAssociation, cancellationToken);

    public Task<int> DeleteCatalogMovieAssociationAsync(
        CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken) =>
        _repository.DeleteCatalogMovieAssociationAsync(catalogMovieAssociation, cancellationToken);

    public Task<int> DeleteCatalogMovieAssociationsByCatalogIdAsync(long catalogId, CancellationToken cancellationToken) =>
        _repository.DeleteCatalogMovieAssociationsByCatalogIdAsync(catalogId, cancellationToken);
}