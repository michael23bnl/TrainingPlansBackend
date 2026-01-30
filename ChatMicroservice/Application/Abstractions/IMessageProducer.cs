using Shared.DTO;

namespace ChatMicroservice.Application.Abstractions;

public interface IMessageProducer
{
    Task StartProducerAsync(CancellationToken ct);
    Task<List<PlanResponse>> SendMessageAsync(List<Guid> message, CancellationToken ct);
}