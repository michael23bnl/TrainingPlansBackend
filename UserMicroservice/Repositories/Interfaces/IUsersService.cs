using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;

namespace UserMicroservice.Repositories.Interfaces;

public interface IUsersService {
    public Task<Guid> Register(string userName, string email, string password, string role);

    public Task<string> Login(string email, string password);

    public Task<bool> CheckPermissions(string token, PermissionRequirement requirement);

    public Task<(Guid userId, HashSet<Permission>)> GetPermissions(string token);
    
    public Task<string> GetUserName(Guid userId);
}