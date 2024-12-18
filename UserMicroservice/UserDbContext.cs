using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UserMicroservice.Configurations;
using UserMicroservice.Entities;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;

namespace UserMicroservice;

public class UserDbContext(DbContextOptions<UserDbContext> options,
    IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<RoleEntity> Roles { get; set; }
    
    public DbSet<PermissionEntity> Permissions { get; set; }
    
    public DbSet<RolePermissionEntity> RolePermissions { get; set; }
    
    public DbSet<UserRoleEntity> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));

    }
    
}
