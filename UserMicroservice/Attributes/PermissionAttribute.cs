[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute : Attribute
{
    public string Policy { get; }

    public PermissionAttribute(string policy)
    {
        Policy = policy;
    }
}