using Mapster;
using Microsoft.AspNetCore.Mvc;
using VHub.UserActivities.Api.Contracts.Catalogs;
using VHub.UserActivities.Api.Contracts.Controllers;
using VHub.UserActivities.Application.Catalogs.Handlers;

namespace VHub.UserActivities.Host.Controllers;

[ApiController]
[Route("user-activities/catalogs")]
public class CatalogsController(ICatalogsHandler handler) : ControllerBase, ICatalogsController
{
    private readonly ICatalogsHandler _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    [HttpPost("new")]
    public async Task<long> CreateCatalogAsync(
        [FromBody] CreateCatalogRequest catalog, CancellationToken cancellationToken = default)
    {
       return await _handler.CreateCatalogAsync(catalog.Adapt<Application.Contracts.Catalogs.CatalogDto>(), cancellationToken);
    }
    
    [HttpDelete("delete/{catalogId:long}")]
    public async Task DeleteCatalogAsync([FromRoute] long catalogId, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteCatalogAsync(catalogId, cancellationToken);
    }

    [HttpPost("add-movie")]
    public async Task CreateCatalogMovieAssociationAsync(
        [FromBody] CatalogMovieAssociationDto catalogMovieAssociation, CancellationToken cancellationToken = default)
    {
        await _handler.CreateCatalogMovieAssociationAsync(
            catalogMovieAssociation.Adapt<Application.Contracts.Catalogs.CatalogMovieAssociationDto>(), cancellationToken);
    }
    
    [HttpDelete("{catalogId:long}/delete-movie/{movieId}")]
    public async Task<int> DeleteCatalogMovieAssociationAsync(
        [FromRoute] long catalogId, [FromRoute] string movieId, CancellationToken cancellationToken = default)
    {
        var association = new Application.Contracts.Catalogs.CatalogMovieAssociationDto
        {
            CatalogId = catalogId,
            MovieId = movieId,
        };
        return await _handler.DeleteCatalogMovieAssociationAsync(association, cancellationToken = default);
    }

    [HttpDelete("clear/{catalogId:long}")]
    public async Task<int> DeleteCatalogMovieAssociationsByCatalogIdAsync(
        [FromRoute] long catalogId, CancellationToken cancellationToken)
    {
        return await _handler.DeleteCatalogMovieAssociationsByCatalogIdAsync(catalogId, cancellationToken);
    }
}