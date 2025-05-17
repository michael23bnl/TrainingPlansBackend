using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using StatisticsMicroservice.Services.Interfaces;
using StatisticsMicroservice.Services.RabbitMQ.Connection;

namespace StatisticsMicroservice.Services.RabbitMQ;

public class RabbitMqSubscriber : IMessageSubscriber
{
    
    private readonly IRabbitMqConnection _connection;
    private readonly IStatisticsService _statisticsService;

    public RabbitMqSubscriber(IRabbitMqConnection connection, 
        IStatisticsService statisticsService)
    {
        _connection = connection;
        _statisticsService = statisticsService;
    }

    public async Task ReceiveMessage(CancellationToken stoppingToken)
    {
        await using var channel = await _connection.Connection.CreateChannelAsync(null, stoppingToken);
        
        await channel.QueueDeclareAsync(
            queue: "statistics", 
            durable: false, 
            exclusive: false, 
            autoDelete: false,
            arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            await _statisticsService.SetStatistics(message);
        };
            
        await channel.BasicConsumeAsync(
            "statistics", 
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