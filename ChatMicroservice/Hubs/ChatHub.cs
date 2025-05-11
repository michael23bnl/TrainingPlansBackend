using System.Text.Json;
using ChatMicroservice.Contracts;
using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatMicroservice.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string? message, 
        List<Plan>? plans, DateTime sendingDate);
}
public class ChatHub : Hub<IChatClient>
{

    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IChatRepository _chatRepository;

    public ChatHub(IDistributedCache cache, 
        IHttpContextAccessor httpContextAccessor, 
        IChatRepository chatRepository)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _chatRepository = chatRepository;
    }
    
    private string? GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Id"].ToString();
        return userId;
    }
    
    private string? GetUserName()
    {
        var userName = _httpContextAccessor.HttpContext!.Request.Headers["X-User-Name"].ToString();
        return userName;
    }
    
    public async Task<IResult> JoinChat(UserConnection connection)
    {
        var userId = GetUserId();

        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        if (!roomList.Contains(connection.ChatRoom)) 
        {
            roomList.Add(connection.ChatRoom);
            await _cache.SetStringAsync(userId, JsonSerializer.Serialize(roomList));
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
            var message = $"{GetUserName()} присоединился к чату";
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = connection.ChatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow
            };
            await Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage(chatMessage.UserName, chatMessage.Message, null, chatMessage.SendingDate);
            await _chatRepository.SaveMessageAsync(chatMessage);
            return Results.Ok();
        }
        
        return Results.BadRequest();
    }

    public async Task<List<MessageResponse>> GetPreviousMessages(string chatRoom)
    {
        var previousMessages = await _chatRepository.GetMessagesByRoomAsync(chatRoom);

        var response = previousMessages.Select(pm => new MessageResponse(
                pm.UserName,
                pm.Message,
                pm.Plans,
                pm.SendingDate
            )
        ).ToList();
        
        return response;
    }

    public async Task SendMessage(string? message, List<Plan>? plans, string chatRoom)
    {
        var userId = GetUserId();

        var chatRooms = await _cache.GetStringAsync(userId);
        var roomList = chatRooms != null
            ? JsonSerializer.Deserialize<List<string>>(chatRooms)
            : new List<string>();
        
        if (!roomList.Contains(chatRoom))
        {
            throw new Exception("User is not a member of this chat room.");
        }

        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = GetUserName() ?? "Unknown User",
            ChatRoom = chatRoom,
            Message = message,
            SendingDate = DateTime.UtcNow,
            Plans = plans.Select(p => new Plan {
                Category = p.Category,
                Exercises = p.Exercises.Select(e => new Exercise {
                    Name = e.Name,
                    MuscleGroup = e.MuscleGroup
                }).ToList()
            }).ToList(),
        };
        
        await _chatRepository.SaveMessageAsync(chatMessage);
        await Clients
            .Group(chatRoom)
            .ReceiveMessage(GetUserName() ?? "Unknown User", message, plans, chatMessage.SendingDate);
    }

    public async Task LeaveChat(string chatRoom)
    {
        var userId = GetUserId();
        
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();
        
        if (roomList.Contains(chatRoom))
        {
            roomList.Remove(chatRoom);
            await _cache.SetStringAsync(userId, JsonSerializer.Serialize(roomList));
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoom);
            var message = $"{GetUserName() ?? "Unknown User"} покинул чат {chatRoom}";
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = chatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow,
            };
            await _chatRepository.SaveMessageAsync(chatMessage);
            await Clients.Groups(chatRoom)
                .ReceiveMessage(chatMessage.UserName, chatMessage.Message, null, chatMessage.SendingDate);
        }
    }

    public async Task<List<string>> GetChatGroups()
    {
        var userId = GetUserId();
        
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();
        
        return roomList;
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        foreach (var room in roomList)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
        }

        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();
        var chatRooms = await _cache.GetStringAsync(userId);
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        foreach (var room in roomList)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
        }

        await base.OnDisconnectedAsync(exception);
    }
    
}