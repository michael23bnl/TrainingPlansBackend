using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMq.Connection;

namespace ApiGateway.Services.RabbitMq;

public class RabbitMqProducer : IMessageProducer
{
    private readonly IRabbitMqConnection _connection;
    
    public RabbitMqProducer(IRabbitMqConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<string> SendMessageAsync<T>(T message)
    {
        await using var channel = await _connection.Connection.CreateChannelAsync();
        
        var replyQueue = await channel.QueueDeclareAsync(
            queue: "", 
            exclusive: true);
        await channel.QueueDeclareAsync("request-queue", exclusive: false);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        var tcs = new TaskCompletionSource<string>();
        
        consumer.ReceivedAsync += (sender, args) =>
        {
            var body = args.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);
            tcs.SetResult(response);
            return Task.CompletedTask;
        };
        
        await channel.BasicConsumeAsync(queue: replyQueue.QueueName, autoAck: true, consumer: consumer);
        
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        
        var properties = new BasicProperties();
        properties.ReplyTo = replyQueue.QueueName;
        properties.CorrelationId = Guid.NewGuid().ToString();
        
        await channel.BasicPublishAsync(
            exchange: string.Empty, 
            routingKey: "request-queue", 
            mandatory: false, 
            basicProperties: properties, 
            body: body);
        
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        cts.Token.Register(() => tcs.TrySetCanceled());
        return await tcs.Task;
    }
}