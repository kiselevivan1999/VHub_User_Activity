using VHub.UserActivities.Application.Contracts.Reviews;

namespace VHub.UserActivities.Application.Reviews.Repositories;

public interface IReviewsRepository
{
    Task<long> CreateReviewAsync(ReviewDto review, CancellationToken cancellationToken);
   
    Task DeleteReviewAsync(long id, CancellationToken cancellationToken);
    
    Task CreateReviewLikeAsync(ReviewLikeDto reviewLike, CancellationToken cancellationToken);
    
    Task DeleteReviewLikeAsync(Guid appraiserId, long reviewId, CancellationToken cancellationToken);
}