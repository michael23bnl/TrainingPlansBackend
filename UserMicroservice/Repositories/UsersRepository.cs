using Microsoft.EntityFrameworkCore;
using UserMicroservice.Entities;
using UserMicroservice.Enums;
using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Repositories;

public class UsersRepository : IUsersRepository {

    private readonly UserDbContext _context;


    public UsersRepository(UserDbContext context) { 
        _context = context;
    }

    public async Task<Guid> Create(UserModel userModel)
    {
        
        var roleEntity = await _context.Roles
                             .SingleOrDefaultAsync(r => r.Id == (int)Role.User)
                         ?? throw new InvalidOperationException();

        var userEntity = new UserEntity() {
            Id = userModel.Id,
            UserName = userModel.UserName,
            PasswordHash = userModel.PasswordHash,
            Email = userModel.Email,
            Roles = [roleEntity]
        };
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userModel.Id;
    }

    public async Task<UserModel?> GetByEmail(string email)
    {

        var userEntity = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);

        return userEntity != null 
            ? UserModel.Create(
                userEntity.Id, 
                userEntity.UserName, 
                userEntity.PasswordHash, 
                userEntity.Email) 
            : null;

    }

    public async Task<string> GetUserNameById(Guid userId)
    {
        var userName = await _context.Users
            .Where(u => u.Id == userId)
            .Select(u => u.UserName)
            .FirstOrDefaultAsync();
        return userName;
    }

    public async Task<HashSet<Permission>> GetUserPermissions(Guid userId) {
        var roles = await _context.Users
            .AsNoTracking()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToListAsync();

        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(p => (Permission)p.Id)
            .ToHashSet();
    }

}