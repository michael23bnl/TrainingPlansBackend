using System.Text.Json;
using ChatMicroservice.Contracts;
using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using TrainingPlans.Contracts;
using TrainingPlans.Models;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Infrastructure;

namespace ChatMicroservice.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string message, DateTime date);
    
    public Task ReceiveMessage(string userName, string? message, 
        List<PreparedPlanResponse>? plans, DateTime sendingDate);
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
            string message = $"{GetUserName()} присоединился к чату";
            await Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage("System", message, DateTime.UtcNow);
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = connection.ChatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow,
            };
            await _chatRepository.SaveMessageAsync(chatMessage);
            return Results.Ok();
        }
        
        return Results.BadRequest();
    }

    public async Task<List<MessageResponse>> GetPreviousMessages(string chatRoom)
    {
        var previousMessages = await _chatRepository.GetMessagesByRoomAsync(chatRoom);

        return previousMessages.Select(pm => new MessageResponse(
                pm.UserName,
                pm.Message,
                pm.SendingDate.ToString()
            )
        ).ToList();
    }

    public async Task SendMessage(string? message, List<PreparedPlanResponse>? plans, string chatRoom)
    {
        var userId = GetUserId();

        var chatRooms = await _cache.GetStringAsync(userId);
        var roomList = chatRooms != null
            ? JsonSerializer.Deserialize<List<string>>(chatRooms)
            : new List<string>();

        var a = new PreparedPlanResponse(Guid.NewGuid(), "", new List<ExerciseResponse>(), false);

        if (!roomList.Contains(chatRoom))
        {
            throw new Exception("User is not a member of this chat room.");
        }

        var chatMessage = new ChatPlanMessage
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = GetUserName() ?? "Unknown User",
            ChatRoom = chatRoom,
            Message = message,
            SendingDate = DateTime.UtcNow,
            Plans = plans.Select(p => new PreparedPlanResponse(
                p.Id,
                p.Category,
                p.Exercises.Select(e => new ExerciseResponse(
                    e.Id,
                    e.Name,
                    e.MuscleGroup
                )).ToList(),
                p.IsFavorite
            )).ToList(),
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
            await Clients.Groups(chatRoom)
                .ReceiveMessage(GetUserName() ?? "Unknown User", $"покинул чат {chatRoom}", DateTime.UtcNow);
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