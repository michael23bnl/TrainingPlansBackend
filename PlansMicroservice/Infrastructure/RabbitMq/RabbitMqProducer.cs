using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using TrainingPlans.Infrastructure.RabbitMq.Connection;

namespace TrainingPlans.Infrastructure.RabbitMq;

public class RabbitMqProducer : IMessageProducer
{
    
    private readonly IRabbitMqConnection _connection;

    public RabbitMqProducer(IRabbitMqConnection connection)
    {
        _connection = connection;
    }

    public async Task SendMessage<T>(T message, string queue)
    {
        var channel = await _connection.Connection.CreateChannelAsync();
        
        await channel.QueueDeclareAsync(
            queue: queue, 
            durable: false, 
            exclusive: false, 
            autoDelete: false,
            arguments: null);
        
        var json = JsonSerializer.Serialize(message);
        
        var body = Encoding.UTF8.GetBytes(json);
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, 
            routingKey: queue, 
            mandatory: false, 
            body: body);
    }
    
}