using Microsoft.AspNetCore.Authorization;
using UserMicroservice.Enums;

namespace UserMicroservice.Infrastructure;

public class PermissionRequirement : IAuthorizationRequirement {

    public PermissionRequirement(Permission[] permissions) { 
        Permissions = permissions;
    }
    public Permission[] Permissions { get; set; } = [];
}