namespace ChatMicroservice.Application.Abstractions;

public interface IUserChatRoomCache
{
    Task<List<string>> GetUserChatRoomsAsync(string userId, CancellationToken ct);
    Task SetUserChatRoomsAsync(string userId, List<string> roomList, CancellationToken ct);
}