using TrainingPlans.Application.Services.Interfaces;
using TrainingPlans.Domain.Models;
using TrainingPlans.Infrastructure.RabbitMq;
using TrainingPlans.Pagination;
using TrainingPlans.Persistence.Repositories.Interfaces;

namespace TrainingPlans.Application.Services;

public class CompletedPlansService : ICompletedPlansService
{
    private readonly ICompletedPlansRepository _completedPlansRepository;
    private readonly IMessageProducer _producer;
    
    public CompletedPlansService(ICompletedPlansRepository completedPlansRepository,
        IMessageProducer producer)
    {
        _completedPlansRepository = completedPlansRepository;
        _producer = producer;
    }
    
    public async Task MarkAsCompleted(Guid userId, Guid planId)
    {
        await _completedPlansRepository.MarkAsCompleted(userId, planId);
        var plan = await _completedPlansRepository.GetCompletedPlan(userId, planId);
        var data = new
        {
            UserId = userId,
            PlanId = plan.Plan.Id,
            Exercises = plan.Plan.Exercises
                .Select(x => new
                {
                    Name = x.Name,
                    MuscleGroup = x.MuscleGroup
                }).ToList(),
            CompletionDate = plan.CompletionDate,
        };
        await _producer.SendMessage(data, "statistics.create");
    }
    
    public async Task RemoveCompletedMark(Guid userId, Guid planId)
    {
        await _completedPlansRepository.RemoveCompletedMark(userId, planId);

        var data = new
        {
            UserId = userId,
            PlanId = planId
        };
        
        await _producer.SendMessage(data, "statistics.delete");
    }
    
    public async Task<List<CompletedPlanModel>> GetCompletedPlans(Guid userId)
    {
        return await _completedPlansRepository.GetCompletedPlans(userId);
    }
    
    public async Task<(int, List<PlanModel>)> GetCompletedPlans(Guid userId, PlanParameters planParameters)
    {
        return await _completedPlansRepository.GetCompletedPlansPaginated(userId, planParameters);
    }
}