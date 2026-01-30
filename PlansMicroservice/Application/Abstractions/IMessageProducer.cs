namespace TrainingPlans.Application.Abstractions;

public interface IMessageProducer
{
    Task SendMessageAsync<T>(T message, string queue);
}