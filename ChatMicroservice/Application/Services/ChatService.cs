using System.Text.Json;
using ChatMicroservice.API.DTO;
using ChatMicroservice.Contracts;
using ChatMicroservice.Infrastructure.Hubs;
using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatMicroservice.Application.Services;

public class ChatService : IChatService
{   
    
    private readonly IDistributedCache _cache;
    private readonly IChatRepository _chatRepository;
    private readonly IHubContext<ChatHub, IChatClient> _hubContext;

    public ChatService(
        IDistributedCache cache, 
        IChatRepository chatRepository,
        IHubContext<ChatHub, IChatClient> hubContext)
    {
        _cache = cache;
        _chatRepository = chatRepository;
        _hubContext = hubContext;
    }

    public async Task<IResult> JoinChat(string userId, string userName, string connectionId, UserConnection connection)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        if (roomList.Contains(connection.ChatRoom))
        {
            return Results.BadRequest();
        }

        roomList.Add(connection.ChatRoom);
            await _cache.SetStringAsync(userId, JsonSerializer.Serialize(roomList));
            await _hubContext.Groups.AddToGroupAsync(connectionId, connection.ChatRoom);
            var message = $"{userName} присоединился к чату";
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = connection.ChatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow
            };
            await _hubContext.Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage(chatMessage.UserName, chatMessage.Message, null, chatMessage.SendingDate);
            await _chatRepository.SaveMessageAsync(chatMessage);
            return Results.Ok();
    }
    
    public async Task LeaveChat(string chatRoom, string userId, string userName, string connectionId)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();
        
        if (roomList.Contains(chatRoom))
        {
            roomList.Remove(chatRoom);
            await _cache.SetStringAsync(userId, JsonSerializer.Serialize(roomList));
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatRoom);
            var message = $"{userName} покинул чат {chatRoom}";
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                UserName = "System",
                ChatRoom = chatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow,
            };
            await SaveMessageAsync(chatMessage);
            await _hubContext.Clients.Groups(chatRoom)
                .ReceiveMessage(chatMessage.UserName, chatMessage.Message, null, chatMessage.SendingDate);
        }
    }
    
    public async Task ConnectedAsync(string userId, string connectionId)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        foreach (var room in roomList)
        {
            await _hubContext.Groups.AddToGroupAsync(connectionId, room);
        }
    }
    
    public async Task DisconnectedAsync(string userId, string connectionId)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        foreach (var room in roomList)
        {
            await _hubContext.Groups.RemoveFromGroupAsync(connectionId, room);
        }
    }

    public async Task SendMessage(string? message, List<Plan>? plans, 
        string chatRoom, string userId, string userName)
    {

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
            UserName = userName,
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
        
        await SaveMessageAsync(chatMessage);
        await _hubContext.Clients
            .Group(chatRoom)
            .ReceiveMessage(userName, message, plans, chatMessage.SendingDate);
    }
    
    public async Task SaveMessageAsync(ChatMessage message)
    {
        await _chatRepository.SaveMessageAsync(message);
    }
    
    public async Task<List<string>> GetChatRooms(string userId)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();
        
        return roomList;
    }
    
    public async Task<Dictionary<string, LastMessageResponse>> GetChatRoomsWithLastMessages(string userId)
    {
        var chatRooms = await _cache.GetStringAsync(userId);
        
        var roomList = chatRooms != null 
            ? JsonSerializer.Deserialize<List<string>>(chatRooms) 
            : new List<string>();

        var roomLastMessage = new Dictionary<string, LastMessageResponse>();

        foreach (var room in roomList)
        {
            var lastMessage = await _chatRepository.GetRoomLastMessage(room);
            roomLastMessage.Add(room, lastMessage);
        }
        
        return roomLastMessage;
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
}