using Mapster;
using Microsoft.AspNetCore.Mvc;
using VHub.UserActivities.Api.Contracts.Controllers;
using VHub.UserActivities.Api.Contracts.MovieRates;
using VHub.UserActivities.Application.Contracts.MovieRates;
using VHub.UserActivities.Application.MovieRates.Handlers;

namespace VHub.UserActivities.Host.Controllers;

[ApiController]
[Route("user-activities/movie-rates")]
public class MovieRatesController(IMovieRatesHandler handler) : ControllerBase, IMovieRatesController
{
    private readonly IMovieRatesHandler _handler = handler ?? throw new ArgumentNullException(nameof(handler));

    [HttpPost("new")]
    public async Task CreateMovieRateAsync([FromBody] CreateMovieRateRequest request, CancellationToken cancellationToken = default)
    {
        await _handler.CreateMovieRateAsync(request.Adapt<MovieRateDto>(), cancellationToken);
    }

    [HttpDelete("delete")]
    public async Task DeleteMovieRateAsync([FromQuery] Guid appraiserId, [FromQuery] string movieId, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteMovieRateAsync(appraiserId, movieId, cancellationToken);
    }
}