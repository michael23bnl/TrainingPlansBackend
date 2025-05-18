namespace StatisticsMicroservice.Services.RabbitMQ;

public interface IMessageSubscriber
{
    public Task ReceiveMessage(Func<string, Task> messageHandler,
        string queue,
        CancellationToken stoppingToken);
}