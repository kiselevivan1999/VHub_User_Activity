using System.Security.Claims;

namespace VHub.UserActivities.Host.Extensions;

public static class HttpContextAccessorHalper
{
    public static Guid GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext == null || httpContextAccessor.HttpContext.User == null)
            throw new InvalidOperationException("HttpContext или HttpContext.User равны null.");

        var userIdValue = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new ArgumentNullException("Не найден идентификатор пользователя");

        if (Guid.TryParse(userIdValue, out var userId) is false)
            throw new ArgumentException("Не удалось преобразовать строку в Guid");

        return userId;
    }
}
