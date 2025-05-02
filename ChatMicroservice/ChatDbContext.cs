using System.Text.Json;
using ChatMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatMicroservice;

public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMessage>(b =>
        {
            b.Property(x => x.Plans)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions 
                    { 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        WriteIndented = false
                    }),
                    v => JsonSerializer.Deserialize<List<Plan>>(v, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }))
                .HasColumnType("jsonb"); // Для PostgreSQL
        });
    }
}