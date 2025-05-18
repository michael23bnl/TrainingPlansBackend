namespace TrainingPlans.Infrastructure.RabbitMq;

public interface IMessageProducer
{
    Task SendMessage<T>(T message, string queue);
}