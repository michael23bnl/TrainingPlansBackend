namespace UserMicroservice.Services.RabbitMq;

public interface IMessageSubscriber
{
    Task ReceiveMessage(CancellationToken stoppingToken);
    
}