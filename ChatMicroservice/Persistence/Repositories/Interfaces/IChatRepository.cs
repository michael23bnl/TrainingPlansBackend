using ChatMicroservice.API.DTO;
using ChatMicroservice.Models;

namespace ChatMicroservice.Repositories.Interfaces;

public interface IChatRepository
{
    public Task SaveMessageAsync(ChatMessage message);
    
    public Task<List<ChatMessage>> GetMessagesByRoomAsync(string chatRoom, int limit = 50);

    public Task<ChatMessage> GetRoomLastMessage(string chatRoom);

    // public Task DeleteMessageHistory(string chatRoom);
}