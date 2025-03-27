namespace UserMicroservice.Infrastructure;

public class AuthorizationOptions {
    public RolePermissions[] RolePermissions { get; set; } = [];
}
public class RolePermissions { // названия полей должны совпадать с названиями полей в файле appsettings.json
    public string Role { get; set; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}