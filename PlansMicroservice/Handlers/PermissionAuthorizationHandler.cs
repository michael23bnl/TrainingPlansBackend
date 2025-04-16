/*using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Repositories.Interfaces;

namespace TrainingPlans.Handlers;

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
        
        var permission = _httpContextAccessor.HttpContext.Request.Headers["Permission"];
        if (permission.Intersect(requirement.Permissions).Any())
        {
            context.Succeed(requirement);
        }
    }
}*/