using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using ChatMicroservice.Application.Abstractions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.DTO;
using Shared.RabbitMq.Connection;

namespace ChatMicroservice.Infrastructure.RabbitMq;

public class RabbitMqProducer : IMessageProducer
{
    private const string QueueName = "plans_queue";
    private IChannel? _channel;
    private string? _replyQueueName;
    private readonly IRabbitMqConnection _connection;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<byte[]>> _callbackMapper
        = new();
    
    public RabbitMqProducer(IRabbitMqConnection connection)
    {
        _connection = connection;
    }
    
    public async Task StartProducerAsync(CancellationToken ct)
    {
        _channel = await _connection.Connection.CreateChannelAsync(options: null, cancellationToken: ct);

        var queueDeclareResult = await _channel.QueueDeclareAsync(cancellationToken: ct);
        _replyQueueName = queueDeclareResult.QueueName;
        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += (_, ea) =>
        {
            var correlationId = ea.BasicProperties.CorrelationId;

            if (!string.IsNullOrEmpty(correlationId))
            {
                if (_callbackMapper.TryRemove(correlationId, out var tcs))
                {
                    var body = ea.Body.ToArray();
                    tcs.TrySetResult(body);
                }
            }

            return Task.CompletedTask;
        };

        await _channel.BasicConsumeAsync(queue: _replyQueueName, autoAck: true, 
            consumer: consumer, cancellationToken: ct);
    }

    public async Task<List<PlanResponse>> SendMessageAsync(List<Guid> message,
        CancellationToken ct)
    {
        if (_channel is null)
        {
            throw new InvalidOperationException();
        }

        var correlationId = Guid.NewGuid().ToString();
        var props = new BasicProperties
        {
            CorrelationId = correlationId,
            ReplyTo = _replyQueueName
        };
        var tcs = new TaskCompletionSource<byte[]>(TaskCreationOptions.RunContinuationsAsynchronously);
        
        _callbackMapper.TryAdd(correlationId, tcs);
        
        var messageJson = JsonSerializer.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);
        
        await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: QueueName,
            mandatory: true, basicProperties: props, body: messageBytes, cancellationToken: ct);

        await using CancellationTokenRegistration ctr = ct
            .Register(() =>
            {
                _callbackMapper.TryRemove(correlationId, out _);
                tcs.SetCanceled();
            });

        var responseBytes = await tcs.Task;
        var response = JsonSerializer.Deserialize<List<PlanResponse>>(responseBytes);

        return response;
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            await _channel.CloseAsync();
        }
    }
}