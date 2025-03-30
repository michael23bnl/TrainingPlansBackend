using System.Text.Json;
using ChatMicroservice.Contracts;
using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using TrainingPlans.Models;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Infrastructure;

namespace ChatMicroservice.Hubs;

public interface IChatClient
{
    public Task RecieveMessage(string userName, string message);
    
    public Task RecieveMessage(string userName, List<PlanModel> plans, string? message);
}

public class ChatHub : Hub<IChatClient>
{

    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;
    private readonly IChatRepository _chatRepository;

    public ChatHub(IDistributedCache cache, 
        IHttpContextAccessor httpContextAccessor, 
        IJwtExtractor jwtExtractor,
        IChatRepository chatRepository)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
        _chatRepository = chatRepository;
    }
    
    private string GetUserId()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        return Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();
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
            string message = $"{connection.UserName} присоединился к чату {connection.ChatRoom}";
            await Clients
                .Group(connection.ChatRoom)
                .RecieveMessage("System", message);
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = connection.ChatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow
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

    public async Task SendMessage(string message, string chatRoom)
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
            UserName = Context.UserIdentifier ?? "Unknown User",
            ChatRoom = chatRoom,
            Message = message,
            SendingDate = DateTime.UtcNow
        };
        
        await _chatRepository.SaveMessageAsync(chatMessage);
        await Clients
            .Group(chatRoom)
            .RecieveMessage(Context.UserIdentifier ?? "Unknown User", message);

    }

    public async Task SendPlan(List<PlanModel> plans, string? message, string chatRoom)
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
        
        var chatMessage = new ChatPlanMessage
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = Context.UserIdentifier ?? "Unknown User",
            ChatRoom = chatRoom,
            Message = message,
            SendingDate = DateTime.UtcNow,
            Plans = plans.Select(p => PlanModel.Create(p.Id, p.Category, 
                        p.Exercises.Select(e => ExerciseModel
                            .Create(
                                e.Id, 
                                e.Name,
                                e.MuscleGroup,
                                e.IsPreMade
                            ).exerciseModel).ToList()!,
                        p.CreatedBy).planModel).ToList()!
        };
        
        await _chatRepository.SaveMessageAsync(chatMessage);
        await Clients
            .Group(chatRoom)
            .RecieveMessage(Context.UserIdentifier ?? "Unknown User", plans, message);
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
                .RecieveMessage("Пользователь", $"покинул чат {chatRoom}");
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