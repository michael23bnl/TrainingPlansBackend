namespace Shared.Auth;

public interface IUserContextService
{
    Guid GetUserId();
    string GetUserName();
}