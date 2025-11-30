using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VHub.UserActivities.Api.Contracts.Controllers;
using VHub.UserActivities.Api.Contracts.Reviews;
using VHub.UserActivities.Application.Contracts.Reviews;
using VHub.UserActivities.Application.Reviews.Handlers;

namespace VHub.UserActivities.Host.Controllers;

[ApiController]
[Authorize]
[Route("user-activities/reviews")]
public class ReviewsController(IReviewsHandler handler, IHttpContextAccessor httpContextAccessor) : ControllerBase, IReviewsController
{
    private readonly IReviewsHandler _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

    [HttpPost("new")]
    public async Task<long> CreateReviewAsync(
        [FromBody] CreateReviewRequest request, CancellationToken cancellationToken = default)
    {
        var userIdClaim = User.FindFirst("sub")?.Value;
        
        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new Exception("User ID not found in token");
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new Exception("Invalid user ID format");
        }
        return await _handler.CreateReviewAsync(request.Adapt<ReviewDto>(), cancellationToken);
    }

    [HttpDelete("delete/{id:long}")]
    public async Task DeleteReviewAsync([FromRoute] long id, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteReviewAsync(id, cancellationToken);
    }

    [HttpPost("like")]
    public async Task LikeReviewAsync(
        [FromBody] LikeReviewRequest request, CancellationToken cancellationToken = default)
    {
        await _handler.LikeReviewAsync(request.Adapt<ReviewLikeDto>(), cancellationToken);
    }

    [HttpDelete("like/delete")]
    public async Task DeleteReviewLikeAsync(
        [FromQuery] Guid appraiserId, [FromQuery] long reviewId, CancellationToken cancellationToken = default)
    {
        await _handler.DeleteReviewLikeAsync(appraiserId, reviewId, cancellationToken);
    }
    
    [HttpGet("debug-claims")]
    [AllowAnonymous] // Чтобы точно сработало
    public IActionResult DebugClaims()
    {
        var authHeader = Request.Headers["Authorization"].FirstOrDefault();
        var result = new
        {
            HasAuthHeader = !string.IsNullOrEmpty(authHeader),
            AuthHeader = authHeader,
            UserAuthenticated = User?.Identity?.IsAuthenticated ?? false,
            ClaimsCount = User?.Claims.Count() ?? 0,
            AllClaims = User?.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList()
        };

        return Ok(result);
    }
}