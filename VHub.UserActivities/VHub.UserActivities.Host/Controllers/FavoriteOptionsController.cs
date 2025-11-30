using Mapster;
using Microsoft.AspNetCore.Mvc;
using VHub.UserActivities.Api.Contracts.Controllers;
using VHub.UserActivities.Api.Contracts.Enums;
using VHub.UserActivities.Api.Contracts.FavoriteOptions;
using VHub.UserActivities.Application.FavoriteOptions.Handlers;

namespace VHub.UserActivities.Host.Controllers;

[ApiController]
[Route("user-activities/favorite-options")]
public class FavoriteOptionsController(IFavoriteOptionsHandler handler) : ControllerBase, IFavoriteOptionsController
{
    private readonly IFavoriteOptionsHandler _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    [HttpPost("add-favorite-genre")]
    public async Task AddFavoriteGenreToUserAsync(
        [FromBody] AddFavoriteGenreToUserRequest request, CancellationToken cancellationToken = default)
    {
        await _handler.AddFavoriteGenreToUserAsync(
            request.UserId, (Common.Enums.GenreType)request.GenreType, cancellationToken);
    }

    [HttpDelete("delete-favorite-genre")]
    public async Task DeleteFavoriteGenreFromUserAsync(
        [FromQuery] Guid userId, [FromQuery] GenreType genreType, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteFavoriteGenreFromUserAsync(
            userId, (Common.Enums.GenreType)genreType, cancellationToken);
    }

    [HttpPost("add-favorite-person")]
    public async Task AddFavoritePersonToUserAsync(
        [FromBody] AddFavoritePersonToUserRequest request, CancellationToken cancellationToken = default)
    {
        await _handler.AddFavoritePersonToUserAsync(
            request.UserId, request.PersonId, cancellationToken);
    }

    [HttpDelete("delete-favorite-person")]
    public async Task DeleteFavoritePersonFromUserAsync(
        [FromQuery] Guid userId, [FromQuery] string personId, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteFavoritePersonFromUserAsync(
            userId, personId, cancellationToken);    }

    [HttpPost("get-user-ids-by-favorite-options")]
    public async Task<Guid[]> GetUserIdsByFavoriteOptionsAsync(
        [FromBody] GetUserIdsByFavoriteOptionsAsyncRequest request, CancellationToken cancellationToken = default)
    {
        return await _handler.GetUserIdsByFavoriteOptionsAsync(
            request.FavoriteGenreTypes.Adapt<Common.Enums.GenreType[]>(), request.FavoritePersonIds, cancellationToken);
    }
}