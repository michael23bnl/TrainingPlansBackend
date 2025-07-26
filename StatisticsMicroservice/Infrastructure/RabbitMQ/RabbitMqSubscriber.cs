using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatisticsMicroservice.Services.Interfaces;
using StatisticsMicroservice.Services.RabbitMQ.Connection;

namespace StatisticsMicroservice.Infrastructure.RabbitMQ;

public class RabbitMqSubscriber : IMessageSubscriber
{
    
    private readonly IRabbitMqConnection _connection;

    public RabbitMqSubscriber(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task ReceiveMessage(Func<string, Task> messageHandler, 
        string queue,
        CancellationToken stoppingToken)
    {
        await using var channel = await _connection.Connection.CreateChannelAsync(null, stoppingToken);
        
        await channel.QueueDeclareAsync(
            queue: queue, 
            durable: false, 
            exclusive: false, 
            autoDelete: false,
            arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            await messageHandler(message);
        };
            
        await channel.BasicConsumeAsync(
            queue, 
            autoAck: true, 
            consumer: consumer);
        
        // используется, чтобы не дать ReceiveMessage завершиться после настройки consumer
        var tcs = new TaskCompletionSource();
        stoppingToken.Register(() => tcs.SetResult());
        await tcs.Task;

        await channel.CloseAsync(stoppingToken);
        await _connection.Connection.CloseAsync(stoppingToken);
    }
}