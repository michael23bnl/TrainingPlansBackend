using Microsoft.AspNetCore.Authorization;
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
}