using TrainingPlans.Contracts;
using TrainingPlans.Models;

namespace ChatMicroservice.Models;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string? UserId { get; set; } // null нужен для сохранения сообщений от системы
    public string UserName { get; set; } = null!;
    public string ChatRoom { get; set; } = null!;
    public string? Message { get; set; } = null;
    public DateTime SendingDate { get; set; }
}

public class ChatPlanMessage : ChatMessage
{
    public List<PreparedPlanResponse>? Plans { get; set; } = new List<PreparedPlanResponse>();
}