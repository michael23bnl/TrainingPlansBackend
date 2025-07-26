using ChatMicroservice.API.DTO;
using ChatMicroservice.Contracts;
using ChatMicroservice.Models;

namespace ChatMicroservice.Application.Services;

public interface IChatService
{
    public Task<IResult> JoinChat(string userId, string userName, string connectionId, UserConnection connection);

    public Task LeaveChat(string chatRoom, string userId, string userName, string connectionId);

    public Task ConnectedAsync(string userId, string connectionId);
    
    public Task DisconnectedAsync(string userId, string connectionId);

    public Task SendMessage(string? message, List<Plan>? plans,
        string chatRoom, string userId, string userName);
    
    public Task SaveMessageAsync(ChatMessage message);

    public Task<List<string>> GetChatRooms(string userId);
    
    public Task<Dictionary<string, ChatMessage>> GetChatRoomsWithLastMessages(string userId);
    
    public Task<List<MessageResponse>> GetPreviousMessages(string chatRoom);
}