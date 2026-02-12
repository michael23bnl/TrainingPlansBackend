namespace TrainingPlans.Infrastructure.DTO;

public record PlanSeed(List<ExerciseSeed> Exercises, Guid? CreatedBy);