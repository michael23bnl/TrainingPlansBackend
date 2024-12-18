using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserMicroservice.Infrastructure;
using UserMicroservice.Models;
using UserMicroservice.Repositories.Interfaces;
namespace UserMicroservice.Services;

public class UsersService : IUsersService {

    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IJwtExtractor _jwtExtractor;
    private readonly IPermissionService _permissionService;

    public UsersService(IUsersRepository usersRepository, 
        IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IJwtExtractor jwtExtractor,
        IPermissionService permissionService) {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
        _jwtExtractor = jwtExtractor;
        _permissionService = permissionService;
    }

    public async Task<Guid> Register(string userName, string email, string password, string role) {
        var hashedPassword = _passwordHasher.Generate(password);
        var userModel = UserModel.Create(Guid.NewGuid(), userName, hashedPassword, email);
        var userId = await _usersRepository.Create(userModel, role);
        return userId;
    }

    public async Task<string> Login(string email, string password) {
        var user = await _usersRepository.GetByEmail(email);
        var result = _passwordHasher.Verify(password, user.PasswordHash);
        if (result == false) {
            throw new Exception("Failed to login");
        }
        var token = _jwtProvider.GenerateToken(user);
        return token;
    }

    public async Task<bool> CheckPermissions(string token, PermissionRequirement requirement)
    {
        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token));
        var userPermissions = await _permissionService.GetPermissionsAsync(userId);
        if (userPermissions.Intersect(requirement.Permissions).Any() || requirement.Permissions.Length == 0)
        {
            return true;
        }

        return false;
    }

}