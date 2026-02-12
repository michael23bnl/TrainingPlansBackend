using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.RabbitMq.Connection;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;

namespace TrainingPlans.Infrastructure.RabbitMq;

public class RabbitMqSubscriber : IMessageSubscriber
{
    private const string QueueName = "plans_queue";
    private readonly IRabbitMqConnection _connection;
    private readonly IPlansService _plansService;

    public RabbitMqSubscriber(IRabbitMqConnection connection, IPlansService plansService)
    {
        _connection = connection;
        _plansService = plansService;
    }
    
    public async Task ReceiveMessageAsync(CancellationToken ct)
    {
        var channel = await _connection.Connection.CreateChannelAsync(options: null, cancellationToken: ct);
        
        await channel.QueueDeclareAsync(queue: QueueName, durable: true, 
            autoDelete: false, exclusive: false, cancellationToken: ct);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (sender, ea) =>
        {
            var cons = (AsyncEventingBasicConsumer)sender;
            var ch = cons.Channel;
            var response = new List<PlanEntity>();
            var props = ea.BasicProperties;
            var replyProps = new BasicProperties
            {
                CorrelationId = props.CorrelationId
            };
            
            try
            {
                var messageJson = ea.Body.ToArray();
                var message = JsonSerializer.Deserialize<List<Guid>>(messageJson);
                //response = await _plansService.GetPlansAsync(message, ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response = new List<PlanEntity>();
            }
            finally
            {
                var responseJson = JsonSerializer.Serialize(response);
                var responseBytes = Encoding.UTF8.GetBytes(responseJson);
                await ch.BasicPublishAsync(exchange: string.Empty, routingKey: props.ReplyTo!, mandatory: true,
                    basicProperties: replyProps, body: responseBytes, cancellationToken: CancellationToken.None);
                await ch.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false, 
                    cancellationToken: CancellationToken.None);
            }
        };
        
        await channel.BasicConsumeAsync(QueueName, autoAck: false, consumer, cancellationToken: ct);
    }
}