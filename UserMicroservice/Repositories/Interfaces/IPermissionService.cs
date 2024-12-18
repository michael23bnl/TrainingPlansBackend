using UserMicroservice.Enums;

namespace UserMicroservice.Repositories.Interfaces;

public interface IPermissionService {
    public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
}