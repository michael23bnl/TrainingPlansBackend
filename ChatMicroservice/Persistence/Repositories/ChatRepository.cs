using ChatMicroservice.Domain.Abstractions;
using ChatMicroservice.Domain.Models;
using MongoDB.Driver;

namespace ChatMicroservice.Persistence.Repositories;

public class ChatRepository : IChatRepository
{
    
    private readonly IMongoCollection<ChatMessage>? _collection;
    
    public ChatRepository(MongoDbService mongoDbService)
    {
        _collection = mongoDbService.Database?.GetCollection<ChatMessage>("ChatMessages");
    }
    
    public async Task SaveMessageAsync(ChatMessage message, CancellationToken ct)
    {
        var options = new InsertOneOptions();

        await _collection.InsertOneAsync(message, options, ct);
    }

    public async Task<List<ChatMessage>> GetMessagesByRoomAsync(string chatRoom, CancellationToken ct, int limit = 50)
    {
        var chatMessages = await _collection
            .Find(cm => cm.ChatRoom == chatRoom)
            .SortBy(cm => cm.SendingDate)
            .Limit(limit)
            .ToListAsync(ct);
        
        return chatMessages;
    }

    public async Task<ChatMessage> GetRoomLastMessage(string chatRoom, CancellationToken ct)
    {
        var lastMessage = await _collection
            .Find(cm => cm.ChatRoom == chatRoom)
            .SortByDescending(cm => cm.SendingDate)
            .ThenByDescending(cm => cm.Id)
            .FirstOrDefaultAsync(ct);
        
        return lastMessage;
    }

    public async Task DeleteMessageHistory(string chatRoom, CancellationToken ct)
    {
        await _collection.DeleteManyAsync(m => m.ChatRoom == chatRoom, ct);
    }
}