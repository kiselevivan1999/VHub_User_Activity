using VHub.UserActivities.Application.Contracts.MovieRates;

namespace VHub.UserActivities.Application.MovieRates.Repositories;

public interface IMovieRatesRepository
{
    Task CreateMovieRateAsync(MovieRateDto movieRate, CancellationToken cancellationToken);
    
    Task DeleteMovieRateAsync(Guid appraiserId, string movieId, CancellationToken cancellationToken);
}