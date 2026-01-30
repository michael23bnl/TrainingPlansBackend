namespace TrainingPlans.Application.Abstractions;

public interface IMessageSubscriber
{
    Task ReceiveMessageAsync(CancellationToken ct);
}