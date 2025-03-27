using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMicroservice.Entities;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;

namespace UserMicroservice.Configurations;

public partial class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity> {


    private readonly AuthorizationOptions _authorizationOptions;

    public RolePermissionConfiguration(AuthorizationOptions authorizationOptions) {
        _authorizationOptions = authorizationOptions;
    }

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder) {

        builder.HasKey(r => new { r.RoleId, r.PermissionId });
        var rolePermissions = ParseRolePermissions();
        builder.HasData(rolePermissions);

    }

    private List<RolePermissionEntity> ParseRolePermissions() {

        return _authorizationOptions.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
            .ToList();
    }
}