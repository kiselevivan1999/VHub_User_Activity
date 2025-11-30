using VHub.UserActivities.Application.Contracts.Reviews;

namespace VHub.UserActivities.Application.Reviews.Handlers;

public interface IReviewsHandler
{
    Task<long> CreateReviewAsync(ReviewDto review, CancellationToken cancellationToken);
   
    Task DeleteReviewAsync(long id, CancellationToken cancellationToken);
    
    Task LikeReviewAsync(ReviewLikeDto reviewLike, CancellationToken cancellationToken);
    
    Task DeleteReviewLikeAsync(Guid appraiserId, long reviewId, CancellationToken cancellationToken);
}