using System.Text.Json;
using ChatMicroservice.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatMicroservice.Infrastructure.Redis;

public class UserChatRoomCache : IUserChatRoomCache
{
    private readonly IDistributedCache _cache;

    public UserChatRoomCache(IDistributedCache cache)
    {
        _cache = cache;
    }
    
    public async Task<List<string>> GetUserChatRoomsAsync(string userId, CancellationToken ct)
    {
        var chatRoomsSerialized = await _cache.GetStringAsync(userId, ct);
        var chatRooms = chatRoomsSerialized is not null 
            ? JsonSerializer.Deserialize<List<string>>(chatRoomsSerialized)
            : new List<string>();
        
        return chatRooms;
    }

    public async Task SetUserChatRoomsAsync(string userId, List<string> chatRooms, CancellationToken ct)
    {
        await _cache.SetStringAsync(userId, JsonSerializer.Serialize(chatRooms), ct);
    }
}