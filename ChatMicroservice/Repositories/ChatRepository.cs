using ChatMicroservice.Models;
using ChatMicroservice.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatMicroservice.Repositories;

public class ChatRepository : IChatRepository
{
    
    private readonly ChatDbContext _context;
    
    public ChatRepository(ChatDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveMessageAsync(ChatMessage message)
    {
        await _context.ChatMessages.AddAsync(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetMessagesByRoomAsync(string chatRoom, int limit = 50)
    {
        var chatMessages = await _context.ChatMessages
            .Where(cm => cm.ChatRoom == chatRoom)
            .OrderBy(cm => cm.SendingDate)
            .ToListAsync();
        return chatMessages;
    }

    public async Task DeleteMessageHistory(string chatRoom)
    {
        var messages = await _context.ChatMessages
            .Where(m => m.ChatRoom == chatRoom)
            .ToListAsync();
        
        _context.RemoveRange(messages);
        await _context.SaveChangesAsync();
    }
}