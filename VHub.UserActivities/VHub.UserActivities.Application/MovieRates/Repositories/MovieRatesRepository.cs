using Mapster;
using Microsoft.EntityFrameworkCore;
using VHub.UserActivities.Application.Contracts.MovieRates;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Domain.Entities.MovieRates;

namespace VHub.UserActivities.Application.MovieRates.Repositories;

internal class MovieRatesRepository(UserActivitiesDbContext dbContext) : IMovieRatesRepository
{
    private readonly UserActivitiesDbContext _dbContext =
        dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CreateMovieRateAsync(MovieRateDto movieRate, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(movieRate.Adapt<MovieRateEntity>(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteMovieRateAsync(Guid appraiserId, string movieId, CancellationToken cancellationToken)
    {
        await _dbContext.MovieRates
            .Where(x => x.AppraiserId == appraiserId && x.MovieId == movieId)
            .ExecuteDeleteAsync(cancellationToken);
    }
}