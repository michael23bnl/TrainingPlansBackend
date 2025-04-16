namespace ApiGateway.Services.RabbitMq;

public interface IMessageProducer
{
    Task<string> SendMessage<T>(T message);
}