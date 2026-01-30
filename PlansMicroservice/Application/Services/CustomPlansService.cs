
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.Application.Services;

public class CustomPlansService : ICustomPlansService
{
    private readonly ICustomPlansRepository _customPlansRepository;

    public CustomPlansService(ICustomPlansRepository customPlansRepository)
    {
        _customPlansRepository = customPlansRepository;
    }
    
    public async Task<Guid> AddOrRemoveCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var customPlanId = await _customPlansRepository.AddOrRemoveAsync(userId, planId, ct);

        return customPlanId;
    }

    public async Task<Guid> CompleteCustomPlanAsync(Guid userId, Guid planId, CancellationToken ct)
    {
        var customPlanId = await _customPlansRepository.CompleteAsync(userId, planId, ct);
        
        return customPlanId;
    }

    public async Task<List<Guid>> GetCompletedPlansIdsAsync(Guid userId, CancellationToken ct)
    {
        var ids = await _customPlansRepository.GetCompletedIdsAsync(userId, ct);

        return ids;
    }
}