using ChatMicroservice.Application.Abstractions;
using ChatMicroservice.Domain.Abstractions;
using ChatMicroservice.Domain.Models;
using Shared.DTO;

namespace ChatMicroservice.Application.Services;

public class ChatService : IChatService
{
    private readonly IUserChatRoomCache _userChatRoomCache;
    private readonly IChatRepository _chatRepository;
    private readonly IChatNotifier _chatNotifier;
    private readonly IMessageProducer _messageProducer;

    public ChatService(
        IChatRepository chatRepository,
        IChatNotifier chatNotifier,
        IUserChatRoomCache userChatRoomCache,
        IMessageProducer messageProducer)
    {
        _chatRepository = chatRepository;
        _chatNotifier = chatNotifier;
        _userChatRoomCache = userChatRoomCache;
        _messageProducer = messageProducer;
    }

    public async Task<IResult> JoinChat(string userId, string userName, string connectionId, string chatRoom, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);

        if (chatRooms.Contains(chatRoom))
        {
            return Results.BadRequest();
        }

        chatRooms.Add(chatRoom);
        await _userChatRoomCache.SetUserChatRoomsAsync(userId, chatRooms, ct);
        await _chatNotifier.AddUserToGroupAsync(connectionId, chatRoom);
        var message = $"{userName} присоединился к чату";
        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid(),
            UserId = null,
            ChatRoom = chatRoom,
            Message = message,
            SendingDate = DateTime.UtcNow
        };
        await _chatNotifier.NotifyGroupAsync(chatRoom, userName, chatMessage.Message, null,
            chatMessage.SendingDate);
        await _chatRepository.SaveMessageAsync(chatMessage, ct);
        return Results.Ok();
    }

    public async Task LeaveChat(string chatRoom, string userId, string userName, string connectionId, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);
        
        if (chatRooms.Contains(chatRoom))
        {
            chatRooms.Remove(chatRoom);
            await _userChatRoomCache.SetUserChatRoomsAsync(userId, chatRooms, ct);
            await _chatNotifier.RemoveUserFromGroupAsync(connectionId, chatRoom);
            var message = $"{userName} покинул чат";
            var chatMessage = new ChatMessage
            {
                Id = Guid.NewGuid(),
                UserId = null,
                ChatRoom = chatRoom,
                Message = message,
                SendingDate = DateTime.UtcNow,
            };
            
            await _chatRepository.SaveMessageAsync(chatMessage, ct);
            await _chatNotifier.NotifyGroupAsync(chatRoom, userName, chatMessage.Message, null,
                chatMessage.SendingDate);
        }
    }
    
    public async Task ConnectedAsync(string userId, string connectionId, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);

        foreach (var room in chatRooms)
        {
            await _chatNotifier.AddUserToGroupAsync(connectionId, room);
        }
    }
    
    public async Task DisconnectedAsync(string userId, string connectionId, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);

        foreach (var room in chatRooms)
        {
            await _chatNotifier.RemoveUserFromGroupAsync(connectionId, room);
        }
    }

    public async Task SendMessage(string? message, List<Guid>? planIds, 
        string chatRoom, string userId, string userName, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);
        
        if (!chatRooms.Contains(chatRoom))
        {
            throw new Exception("User is not a member of this chat room.");
        }

        var chatMessage = new ChatMessage
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ChatRoom = chatRoom,
            Message = message,
            AttachedPlanIds = planIds,
            SendingDate = DateTime.UtcNow,
        };
        var plans = new List<PlanResponse>();

        if (planIds is not null)
        {
            plans = await _messageProducer.SendMessageAsync(planIds, ct);
        }
        
        await _chatRepository.SaveMessageAsync(chatMessage, ct);
        await _chatNotifier.NotifyGroupAsync(chatRoom, userName, message, plans, chatMessage.SendingDate);
    }
    
    public async Task<List<string>> GetChatRooms(string userId, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);
        
        return chatRooms;
    }
    
    public async Task<Dictionary<string, ChatMessage>> GetChatRoomsWithLastMessages(string userId, CancellationToken ct)
    {
        var chatRooms = await _userChatRoomCache.GetUserChatRoomsAsync(userId, ct);

        var roomLastMessage = new Dictionary<string, ChatMessage>();

        foreach (var room in chatRooms)
        {
            var lastMessage = await _chatRepository.GetRoomLastMessage(room, ct);
            roomLastMessage.Add(room, lastMessage);
        }
        
        return roomLastMessage;
    }
    
    public async Task<List<ChatMessage>> GetPreviousMessages(string chatRoom, CancellationToken ct)
    {
        var previousMessages = await _chatRepository.GetMessagesByRoomAsync(chatRoom, ct);
        
        return previousMessages;
    }
}