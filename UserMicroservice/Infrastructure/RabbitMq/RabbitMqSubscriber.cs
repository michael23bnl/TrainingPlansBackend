using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserMicroservice.Enums;
using UserMicroservice.Infrastructure;
using UserMicroservice.Repositories.Interfaces;
using UserMicroservice.Services.RabbitMq.Connection;

namespace UserMicroservice.Services.RabbitMq;

public class RabbitMqSubscriber : IMessageSubscriber
{
    private readonly IRabbitMqConnection _connection;
    private readonly IUsersService _usersService;

    public RabbitMqSubscriber(IRabbitMqConnection connection, IUsersService usersService)
    {
        _connection = connection;
        _usersService = usersService;   
    }

    public async Task ReceiveMessage(CancellationToken stoppingToken)
    {
        
        await using var channel = await _connection.Connection.CreateChannelAsync(null, stoppingToken);
        
        await channel.QueueDeclareAsync(
            "request-queue", 
            exclusive: false, 
            cancellationToken: stoppingToken);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, args) =>
        {
            var body = args.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var token = JsonSerializer.Deserialize<string>(json);
            
            var permissions = await _usersService.GetPermissions(token);

            var userName = await _usersService.GetUserName(permissions.userId);
            
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            };
            
            json = JsonSerializer.Serialize(new {
                    UserId = permissions.Item1,
                    Permissions = permissions.Item2,
                    UserName = userName
                },
                options);
            
            var responseBytes = Encoding.UTF8.GetBytes(json);
    
            await channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: args.BasicProperties.ReplyTo!, 
                body: responseBytes, 
                cancellationToken: stoppingToken);
    };

        await channel.BasicConsumeAsync(
            queue: "request-queue", 
            autoAck: true, 
            consumer: consumer, 
            cancellationToken: stoppingToken);
        
        // используется, чтобы не дать ReceiveMessage завершиться после настройки consumer
        var tcs = new TaskCompletionSource();
        stoppingToken.Register(() => tcs.SetResult());
        await tcs.Task;

        await channel.CloseAsync(stoppingToken);
        await _connection.Connection.CloseAsync(stoppingToken);
        
    }
}