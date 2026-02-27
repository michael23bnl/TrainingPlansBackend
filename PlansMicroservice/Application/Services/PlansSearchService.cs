using Shared.DTO;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Domain.Abstractions;

namespace TrainingPlans.Application.Services;

public class PlansSearchService : IPlansSearchService
{
    private readonly IFullTextSearch _fullTextSearch;

    public PlansSearchService(IFullTextSearch fullTextSearch)
    {
        _fullTextSearch = fullTextSearch;
    }
    
    public async Task<(int totalCount, List<PlanResponse> plans)> SearchPlansAsync(
        string query, PlanParameters parameters, CancellationToken ct)
    {
        var (totalCount, plans) = await _fullTextSearch.SearchAsync(query, null, parameters, ct);
        var planResponse = plans
            .Select(pe => new PlanResponse
            (
                pe.Id,
                pe.Tags,
                pe.Description,
                pe.Exercises
                    .Select(pee => new ExerciseResponse(
                        pee.Id, 
                        pee.Name,
                        pee.MuscleGroup, 
                        pee.Description,
                        pee.Sets,
                        pee.Reps, 
                        pee.Notes))
                    .ToList()
            ))
            .ToList();

        return (totalCount, planResponse);
    }
    
    public async Task<(int totalCount, List<PlanResponse> plans)> SearchCustomPlansAsync(
        string query, Guid userId, PlanParameters parameters, CancellationToken ct)
    {
        var (totalCount, plans) = await _fullTextSearch.SearchAsync(query, userId, parameters, ct);
        var planResponse = plans
            .Select(pe => new PlanResponse
            (
                pe.Id,
                pe.Tags,
                pe.Description,
                pe.Exercises
                    .Select(pee => new ExerciseResponse(
                        pee.Id, 
                        pee.Name,
                        pee.MuscleGroup, 
                        pee.Description,
                        pee.Sets,
                        pee.Reps, 
                        pee.Notes))
                    .ToList()
            ))
            .ToList();

        return (totalCount, planResponse);
    }
}