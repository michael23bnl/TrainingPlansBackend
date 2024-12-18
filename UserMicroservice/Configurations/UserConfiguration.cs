using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserMicroservice.Entities;

namespace UserMicroservice.Configurations;

public partial class UserConfiguration : IEntityTypeConfiguration<UserEntity> {
    public void Configure(EntityTypeBuilder<UserEntity> builder) {

        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).IsRequired();
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.Email).IsRequired();
             
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey(u => u.UserId));
    }
}