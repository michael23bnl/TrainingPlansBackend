using ChatMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatMicroservice;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    public DbSet<ChatMessage> ChatMessages { get; set; }
}