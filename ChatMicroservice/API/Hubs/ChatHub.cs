
using ChatMicroservice.API.DTO;
using ChatMicroservice.Application.Abstractions;
using ChatMicroservice.Domain.Abstractions;
using ChatMicroservice.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Shared.Auth;

namespace ChatMicroservice.API.Hubs;

[Authorize]
public class ChatHub : Hub<IChatClient>
{
    private readonly IChatService _chatService;
    private readonly IUserContextService _userContextService;

    public ChatHub(IChatService chatService, IUserContextService userContextService)
    {
        _chatService = chatService;
        _userContextService = userContextService;
    }
    
    public async Task<IResult> JoinChat(UserConnection connection, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var userName = _userContextService.GetUserName();   
        var connectionId = Context.ConnectionId;
        
        return await _chatService.JoinChat(userId,  connectionId, userName, connection.ChatRoom, ct);
    }

    public async Task SendMessage(string? message, List<Guid>? planIds, string chatRoom, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var userName = _userContextService.GetUserName();   
        
        await _chatService.SendMessage(message, planIds, chatRoom, userId, userName, ct);
    }

    public async Task LeaveChat(string chatRoom, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var userName = _userContextService.GetUserName();   
        var connectionId = Context.ConnectionId;
        
        await _chatService.LeaveChat(chatRoom, userId, userName, connectionId, ct);
    }

    public async Task<List<string>> GetChatRooms(CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var chatRooms = await _chatService.GetChatRooms(userId, ct);
        
        return chatRooms;
    }
    
    public async Task<Dictionary<string, ChatMessage>> GetChatRoomsWithLastMessages(CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var chatRoomsWithLastMessages = await _chatService
            .GetChatRoomsWithLastMessages(userId, ct);
        
        return chatRoomsWithLastMessages;
    }
    
    public async Task<List<ChatMessage>> GetPreviousMessages(string chatRoom, CancellationToken ct)
    {
        var previousMessages = await _chatService.GetPreviousMessages(chatRoom, ct);
        
        return previousMessages;
    }
    
    public async Task OnConnectedAsync(CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var connectionId = Context.ConnectionId;

        await _chatService.ConnectedAsync(userId, connectionId, ct);
        await base.OnConnectedAsync();
    }
    
    public async Task OnDisconnectedAsync(Exception? exception, CancellationToken ct)
    {
        var userId = _userContextService.GetUserId().ToString();
        var connectionId = Context.ConnectionId;

        await _chatService.DisconnectedAsync(userId, connectionId, ct);
        await base.OnDisconnectedAsync(exception);
    }
    
}