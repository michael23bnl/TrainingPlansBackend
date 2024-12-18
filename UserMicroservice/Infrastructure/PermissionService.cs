using UserMicroservice.Enums;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Infrastructure;

public class PermissionService : IPermissionService {
    private readonly IUsersRepository _usersRepository;
    public PermissionService(IUsersRepository usersRepository) {
        _usersRepository = usersRepository;
    }
    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId) {
        return _usersRepository.GetUserPermissions(userId);
    }
}