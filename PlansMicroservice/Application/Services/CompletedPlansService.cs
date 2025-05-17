using TrainingPlans.Infrastructure.RabbitMq;
using TrainingPlans.Models;
using TrainingPlans.Pagination;
using TrainingPlans.Repositories.Interfaces;

namespace TrainingPlans.Application.Services;

public class CompletedPlansService : ICompletedPlansService
{
    private readonly ICompletedPlansRepository _completedPlansRepository;
    private readonly IPlansRepository _plansRepository;
    private readonly IMessageProducer _producer;
    
    public CompletedPlansService(ICompletedPlansRepository completedPlansRepository,
        IPlansRepository plansRepository,
        IMessageProducer producer)
    {
        _completedPlansRepository = completedPlansRepository;
        _plansRepository = plansRepository;
        _producer = producer;
    }
    
    public async Task MarkAsCompleted(Guid userId, Guid planId)
    {
        await _completedPlansRepository.MarkAsCompleted(userId, planId);
        var plan = await _plansRepository.Get(planId, userId);
        var data = new
        {
            exercises = plan.Exercises
                .Select(x => new
                {
                    Name = x.Name,
                    Category = x.MuscleGroup
                }).ToList(),
            userId = userId
        };
        await _producer.SendMessage(data);
    }
    
    public async Task RemoveCompletedMark(Guid userId, Guid planId)
    {
        await _completedPlansRepository.RemoveCompletedMark(userId, planId);
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