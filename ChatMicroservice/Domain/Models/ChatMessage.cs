using ChatMicroservice.Contracts;

namespace ChatMicroservice.Models;

public class ChatMessage
{
    public Guid Id { get; set; }
    public string? UserId { get; set; } // null нужен для сохранения сообщений от системы
    public string UserName { get; set; }
    public string ChatRoom { get; set; }
    public string? Message { get; set; } // сообщение может не содержать текста
    public List<Plan>? Plans { get; set; } // сообщение может не содержать планов
    public DateTime SendingDate { get; set; }
}
