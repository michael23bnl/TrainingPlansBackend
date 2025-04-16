/*using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Infrastructure;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.Claims.FirstOrDefault(
            c => c.Type == CustomClaims.UserId);
        Console.WriteLine($"UserId from token: {userId}");
        if (userId is null || !Guid.TryParse(userId.Value, out var id))
        {
            return;
        }

        using var scope = _serviceScopeFactory.CreateScope();

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permission = await permissionService.GetPermissionsAsync(id);

        if (permission.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}*/

using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Enums;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Infrastructure;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {

        var permission = _httpContextAccessor.HttpContext.Request.Headers["Permission"].ToString();

        var permissionList = permission.Split(',').Select(s => s.Trim()).ToList();
        
        var permissions = new HashSet<Permission>();

        foreach (var permissionName in permissionList)
        {
            permissions.Add((Permission)Enum.Parse(typeof(Permission), permissionName));
        }
        
        if (permissions.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}