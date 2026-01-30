using Shared.DTO;

namespace ChatMicroservice.Application.Abstractions;

public interface IChatNotifier
{
    Task NotifyGroupAsync(string chatRoom, string userName, string? message, List<PlanResponse>? plans, DateTime sendingDate);
    Task AddUserToGroupAsync(string connectionId, string chatRoom);
    Task RemoveUserFromGroupAsync(string connectionId, string chatRoom);
    
}