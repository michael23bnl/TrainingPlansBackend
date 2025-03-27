using UserMicroservice.Infrastructure;

namespace UserMicroservice.Contracts;

public class PayloadRequest
{
    public string Token { get; set; }
    public PermissionRequirement Requirement { get; set; }
}