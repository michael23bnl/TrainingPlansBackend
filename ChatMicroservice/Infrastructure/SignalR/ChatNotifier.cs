
using ChatMicroservice.Application.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Shared.DTO;

namespace ChatMicroservice.Infrastructure.SignalR;

public class ChatNotifier : IChatNotifier
{
    private readonly IHubContext<Hub<IChatClient>, IChatClient> _hubContext;

    public ChatNotifier(IHubContext<Hub<IChatClient>, IChatClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyGroupAsync(string chatRoom, string userName, string? message, List<PlanResponse>? plans, DateTime sendingDate)
    {
        await _hubContext.Clients.Group(chatRoom)
            .ReceiveMessage(userName, message, plans, sendingDate, chatRoom);
    }

    public async Task AddUserToGroupAsync(string connectionId, string chatRoom)
        => await _hubContext.Groups.AddToGroupAsync(connectionId, chatRoom);

    public async Task RemoveUserFromGroupAsync(string connectionId, string chatRoom)
        => await _hubContext.Groups.RemoveFromGroupAsync(connectionId, chatRoom);
}