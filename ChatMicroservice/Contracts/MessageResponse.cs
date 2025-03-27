namespace ChatMicroservice.Contracts;

public record MessageResponse(
    string UserName,
    string Message,
    string SendingSate);
