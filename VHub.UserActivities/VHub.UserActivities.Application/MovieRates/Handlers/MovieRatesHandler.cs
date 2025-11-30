using VHub.UserActivities.Application.Contracts.MovieRates;
using VHub.UserActivities.Application.MovieRates.Repositories;

namespace VHub.UserActivities.Application.MovieRates.Handlers;

internal class MovieRatesHandler(IMovieRatesRepository repository) : IMovieRatesHandler
{
    private readonly IMovieRatesRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task CreateMovieRateAsync(MovieRateDto movieRate, CancellationToken cancellationToken)
    {
        await _repository.CreateMovieRateAsync(movieRate, cancellationToken);
    }

    public async Task DeleteMovieRateAsync(Guid appraiserId, string movieId, CancellationToken cancellationToken)
    {
        await _repository.DeleteMovieRateAsync(appraiserId, movieId, cancellationToken);
    }
}