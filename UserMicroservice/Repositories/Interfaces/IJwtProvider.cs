using UserMicroservice.Models;

namespace UserMicroservice.Repositories.Interfaces;

public interface IJwtProvider {
    public string GenerateToken(UserModel userModel);
}