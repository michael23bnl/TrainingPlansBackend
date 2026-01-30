using Shared.DTO;

namespace ChatMicroservice.Application.Abstractions;

public interface IChatClient
{
    Task ReceiveMessage(string userName, string? message, List<PlanResponse>? plans, DateTime sendingDate, string chatRoom);
}