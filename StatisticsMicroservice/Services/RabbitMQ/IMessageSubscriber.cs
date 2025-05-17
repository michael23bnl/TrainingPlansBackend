namespace StatisticsMicroservice.Services.RabbitMQ;

public interface IMessageSubscriber
{
    public Task ReceiveMessage(CancellationToken stoppingToken);
}