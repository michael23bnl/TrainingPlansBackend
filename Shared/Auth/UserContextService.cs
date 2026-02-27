using Microsoft.AspNetCore.Http;

namespace Shared.Auth;

public class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        //var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"];
        var userId = "088c2dd8-b404-49da-adcc-08c695e9ff55";
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsedId))
            return Guid.Empty;
        
        return parsedId;
    }
    
    public string GetUserName()
    {
        var userName = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Name"].ToString();
        if (string.IsNullOrEmpty(userName))
            return "";
        
        return userName;
    }
}