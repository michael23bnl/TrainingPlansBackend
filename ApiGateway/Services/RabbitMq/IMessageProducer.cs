namespace ApiGateway.Services.RabbitMq;

public interface IMessageProducer
{
    Task<string> SendMessageAsync<T>(T message);
}