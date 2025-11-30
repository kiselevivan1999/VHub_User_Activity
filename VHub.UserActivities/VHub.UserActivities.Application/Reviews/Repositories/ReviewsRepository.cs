using Mapster;
using Microsoft.EntityFrameworkCore;
using VHub.UserActivities.Application.Contracts.Reviews;
using VHub.UserActivities.Database.Configurations;
using VHub.UserActivities.Domain.Entities.Reviews;

namespace VHub.UserActivities.Application.Reviews.Repositories;

internal class ReviewsRepository(UserActivitiesDbContext dbContext) : IReviewsRepository
{
    private readonly UserActivitiesDbContext _dbContext =
        dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<long> CreateReviewAsync(ReviewDto review, CancellationToken cancellationToken)
    {
        var entity = review.Adapt<ReviewEntity>();
        entity.CreatedAt = DateTime.Now;
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }

    public async Task DeleteReviewAsync(long id, CancellationToken cancellationToken) =>
        await _dbContext.Reviews
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

    public async Task CreateReviewLikeAsync(ReviewLikeDto reviewLike, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(reviewLike.Adapt<ReviewLikeEntity>(), cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteReviewLikeAsync(Guid appraiserId, long reviewId, CancellationToken cancellationToken) =>
        await _dbContext.ReviewLikes
            .Where(x => x.AppraiserId == appraiserId && x.ReviewId == reviewId)
            .ExecuteDeleteAsync(cancellationToken);
}