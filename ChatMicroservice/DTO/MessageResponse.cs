
using ChatMicroservice.Models;

namespace ChatMicroservice.Contracts;

public record MessageResponse(
    string UserName,
    string? Message,
    List<Plan>? Plans,
    DateTime SendingDate);
