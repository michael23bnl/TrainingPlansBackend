using System.Text.Json;
using ChatMicroservice.API.DTO;
using ChatMicroservice.Application.Services;
using ChatMicroservice.Contracts;
using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;

namespace ChatMicroservice.Infrastructure.Hubs;

public interface IChatClient
{
    public Task ReceiveMessage(string userName, string? message, 
        List<Plan>? plans, DateTime sendingDate);
}
[Authorize]
public class ChatHub : Hub<IChatClient>
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IChatService _chatService;

    public ChatHub(
        IHttpContextAccessor httpContextAccessor, 
        IChatService chatService)
    {
        _httpContextAccessor = httpContextAccessor;
        _chatService = chatService;
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
        var userId = GetUserId()!;
        var userName = GetUserName()!;   
        var connectionId = Context.ConnectionId;
        return await _chatService.JoinChat(userId, userName, connectionId, connection);
    }

    public async Task SendMessage(string? message, List<Plan>? plans, string chatRoom)
    {
        var userId = GetUserId()!;
        var userName = GetUserName()!;  
        await _chatService.SendMessage(message, plans, chatRoom, userId, userName);
    }

    public async Task LeaveChat(string chatRoom)
    {
        var userId = GetUserId()!;
        var userName = GetUserName()!;   
        var connectionId = Context.ConnectionId;
        await _chatService.LeaveChat(chatRoom, userId, userName, connectionId);
    }

    public async Task<List<string>> GetChatRooms()
    {
        var userId = GetUserId()!;
        var chatRooms = await _chatService.GetChatRooms(userId);
        return chatRooms;
    }
    
    public async Task<Dictionary<string, LastMessageResponse>> GetChatRoomsWithLastMessages()
    {
        var userId = GetUserId()!;
        var chatRoomsWithLastMessages = await _chatService
            .GetChatRoomsWithLastMessages(userId);
        return chatRoomsWithLastMessages;
    }
    
    public async Task<List<MessageResponse>> GetPreviousMessages(string chatRoom)
    {
        var previousMessages = await _chatService.GetPreviousMessages(chatRoom);
        
        return previousMessages;
    }
    
    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId()!;
        var connectionId = Context.ConnectionId;

        await _chatService.ConnectedAsync(userId, connectionId);

        await base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId()!;
        var connectionId = Context.ConnectionId;

        await _chatService.DisconnectedAsync(userId, connectionId);
        
        await base.OnDisconnectedAsync(exception);
    }
    
}