
using ChatMicroservice.Domain.Models;

namespace ChatMicroservice.Domain.Abstractions;

public interface IChatService
{
    public Task<IResult> JoinChat(string userId, string connectionId, string userName, string chatRoom, CancellationToken ct);

    public Task LeaveChat(string chatRoom, string userId, string userName, string connectionId, CancellationToken ct);

    public Task ConnectedAsync(string userId, string connectionId, CancellationToken ct);
    
    public Task DisconnectedAsync(string userId, string connectionId, CancellationToken ct);

    public Task SendMessage(string? message, List<Guid>? planIds,
        string chatRoom, string userId, string userName, CancellationToken ct);
    
    public Task<List<string>> GetChatRooms(string userId, CancellationToken ct);
    
    public Task<Dictionary<string, ChatMessage>> GetChatRoomsWithLastMessages(string userId, CancellationToken ct);
    
    public Task<List<ChatMessage>> GetPreviousMessages(string chatRoom, CancellationToken ct);
}