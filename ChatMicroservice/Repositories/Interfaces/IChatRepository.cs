using ChatMicroservice.Models;

namespace ChatMicroservice.Repositories.Interfaces;

public interface IChatRepository
{
    public Task SaveMessageAsync(ChatMessage message);
    public Task<List<ChatMessage>> GetMessagesByRoomAsync(string chatRoom, int limit = 50);

    public Task DeleteMessageHistory(string chatRoom);
}