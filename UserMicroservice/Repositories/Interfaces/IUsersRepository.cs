using UserMicroservice.Enums;
using UserMicroservice.Models;

namespace UserMicroservice.Repositories.Interfaces;

public interface IUsersRepository {
    public Task<Guid> Create(UserModel userModel, string role);

    public Task<UserModel> GetByEmail(string email);

    public Task<HashSet<Permission>> GetUserPermissions(Guid userId);

}