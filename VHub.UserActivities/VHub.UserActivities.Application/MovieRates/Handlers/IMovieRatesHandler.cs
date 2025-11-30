using VHub.UserActivities.Application.Contracts.MovieRates;

namespace VHub.UserActivities.Application.MovieRates.Handlers;

public interface IMovieRatesHandler
{
    Task CreateMovieRateAsync(MovieRateDto movieRate, CancellationToken cancellationToken);
    
    Task DeleteMovieRateAsync(Guid appraiserId, string movieId, CancellationToken cancellationToken);
}