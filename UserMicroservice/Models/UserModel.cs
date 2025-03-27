namespace UserMicroservice.Models;

public class UserModel {

    private UserModel(Guid id, string userName, string passwordHash, string email) {
        Id = id;
        UserName = userName;
        PasswordHash = passwordHash;
        Email = email;
    }

    public Guid Id { get; set; }

    public string UserName { get; private set; }

    public string PasswordHash { get; private set; }

    public string Email { get; private set; }

    //public ICollection<RoleEntity> Roles { get; set; } 

    public static UserModel Create(Guid id, string userName, string passwordHash, string email) {
        return new UserModel(id, userName, passwordHash, email);
    }
}