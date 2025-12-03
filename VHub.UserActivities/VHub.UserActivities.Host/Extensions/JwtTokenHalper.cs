namespace VHub.UserActivities.Host.Extensions;

public class JwtTokenHalper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtTokenHalper(IHttpContextAccessor httpContextAccessor) 
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId() 
    {
        var a = HttpContextAccessorHalper.GetUserId(_httpContextAccessor);
        return a;
    } 
}
