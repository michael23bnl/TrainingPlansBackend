using System.Text.Json;
using ChatMicroservice.Models;
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
    
    public Task RecieveMessage(string userName, PlanModel plan);
}

public class ChatHub : Hub<IChatClient>
{

    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtExtractor _jwtExtractor;

    public ChatHub(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, IJwtExtractor jwtExtractor)
    {
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _jwtExtractor = jwtExtractor;
    }
    
    public async Task JoinChat(UserConnection connection)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();

        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        if (!roomList.Contains(connection.ChatRoom))
        {
            roomList.Add(connection.ChatRoom);
            await _cache.SetStringAsync(userId, JsonSerializer.Serialize(roomList));
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
        }
        await Clients
            .Groups(connection.ChatRoom)
            .RecieveMessage("System", $"{connection.UserName} присоединился к чату {connection.ChatRoom}");
    }

    public async Task SendMessage(string message, string chatRoom)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();
        
        var chatRooms = await _cache.GetStringAsync(userId);
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        if (roomList.Contains(chatRoom))
        {
            await Clients.Groups(chatRoom).RecieveMessage(Context.UserIdentifier ?? "Unknown User", message);
        }
        else
        {
            throw new Exception("User is not a member of this chat room.");
        }
    }

    public async Task SendPlan(PlanModel plan, string chatRoom)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();
        
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        if (roomList.Contains(chatRoom))
        {
            await Clients.Groups(chatRoom).RecieveMessage(Context.UserIdentifier ?? "Unknown User", plan);
        }
        else
        {
            throw new Exception("User is not a member of this chat room.");
        }
    }

    public async Task LeaveChat(string chatRoom)
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
        var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();
        
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

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception != null)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["suchatastycookie"];
        
            var userId = Guid.Parse(_jwtExtractor.ExtractUserIdFromJwtToken(token)).ToString();
        
            var chatRooms = await _cache.GetStringAsync(userId);
            var roomList = chatRooms != null 
                ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
                : new List<string>();
            
            await _cache.RemoveAsync(userId);
        
            foreach (var chatRoom in roomList)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoom);
                if (chatRoom != null)
                {
                    await Clients
                        .Groups(chatRoom)
                        .RecieveMessage("System", $"{Context.UserIdentifier ?? "Unknown User"} покинул комнату {chatRoom}");
                }
            }
        }
        
        await base.OnDisconnectedAsync(exception);
    }

    /*public async Task JoinChat(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        var stringConnection = JsonSerializer.Serialize(connection);

        await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

        await Clients
            .Groups(connection.ChatRoom)
            .RecieveMessage("Admin", $"{connection.UserName} присоединился к чату");
    }

    public async Task SendMessage(string message)
    {

        var stringConnection = await _cache.GetAsync(Context.ConnectionId);

        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

        if (connection is not null)
        {
            await Clients
                .Groups(connection.ChatRoom)
                .RecieveMessage(connection.UserName, message); 
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {

        var stringConnection = await _cache.GetAsync(Context.ConnectionId);
        var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);
        
        if (connection is not null)
        {
            await _cache.RemoveAsync(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);
            
            await Clients
                .Groups(connection.ChatRoom)
                .RecieveMessage("Admin", $"{connection.UserName} вышел из чата");
        }
        
        await base.OnDisconnectedAsync(exception);
    }*/
}