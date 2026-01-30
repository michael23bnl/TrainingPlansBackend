
using ChatMicroservice.Domain.Models;

namespace ChatMicroservice.Domain.Abstractions;

public interface IChatRepository
{
    public Task SaveMessageAsync(ChatMessage message, CancellationToken ct);
    
    public Task<List<ChatMessage>> GetMessagesByRoomAsync(string chatRoom, CancellationToken ct, int limit = 50);

    public Task<ChatMessage> GetRoomLastMessage(string chatRoom, CancellationToken ct);

    public Task DeleteMessageHistory(string chatRoom, CancellationToken ct);
}