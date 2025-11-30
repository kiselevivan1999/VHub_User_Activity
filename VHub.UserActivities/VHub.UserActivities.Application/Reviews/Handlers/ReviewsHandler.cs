using VHub.UserActivities.Application.Contracts.Reviews;
using VHub.UserActivities.Application.Reviews.Repositories;
 
 namespace VHub.UserActivities.Application.Reviews.Handlers;
 
 internal class ReviewsHandler(IReviewsRepository repository) : IReviewsHandler
 {
     private readonly IReviewsRepository _repository = repository ?? throw new ArgumentNullException(nameof(repository));

     public async Task<long> CreateReviewAsync(ReviewDto review, CancellationToken cancellationToken)
     {
        return await _repository.CreateReviewAsync(review, cancellationToken);
     }

     public async Task DeleteReviewAsync(long id, CancellationToken cancellationToken)
     {
         await _repository.DeleteReviewAsync(id, cancellationToken);
     }

     public async Task LikeReviewAsync(ReviewLikeDto reviewLike, CancellationToken cancellationToken)
     {
         await _repository.CreateReviewLikeAsync(reviewLike, cancellationToken);
     }

     public async Task DeleteReviewLikeAsync(Guid appraiserId, long reviewId, CancellationToken cancellationToken)
     {
         await _repository.DeleteReviewLikeAsync(appraiserId, reviewId, cancellationToken);
     }
 }