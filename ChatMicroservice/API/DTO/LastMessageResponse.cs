namespace ChatMicroservice.API.DTO;

public record LastMessageResponse(
    string Message,
    bool HasPlans);