using System.Data;
using System.Text.Json;
using Dapper;
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Domain.Abstractions;
using TrainingPlans.Domain.Entities;
using Shared.Pagination;

namespace TrainingPlans.Persistence.Repositories;

public class PlansRepository : IPlansRepository
{
    private readonly PlansDbContext _context;

    public PlansRepository(PlansDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(List<Guid> exerciseIds, Guid? createdBy, CancellationToken ct)
    {
        var exercises = await _context.Exercises
            .Where(e => exerciseIds.Contains(e.Id))
            .ToListAsync(ct);
        
        var plan = new PlanEntity
        {
            Id = Guid.NewGuid(),
            Exercises = exercises,
            CreatedBy = createdBy
        };
        
        await _context.Plans.AddAsync(plan, ct);
        await _context.SaveChangesAsync(ct);
        
        return plan.Id;
    }
    
    public async Task<(int, List<PlanEntity>)> GetAllPreloadedAsync(
        PlanParameters planParameters, CancellationToken ct)
    {
        var totalPlanCount = _context.Plans.Count(p => p.CreatedBy == null);
        var plans = await _context.Plans
            .Where(p => p.CreatedBy == null)
            .Include(p => p.Exercises)
            .Skip((planParameters.PageNumber - 1) * planParameters.PageSize)
            .Take(planParameters.PageSize)
            .AsNoTracking()
            .ToListAsync(ct);

        return (totalPlanCount, plans);
    }

    public async Task<List<PlanEntity>> GetAllPreloadedAsync(CancellationToken ct)
    {
        var plans = await _context.Plans
            .Where(p => p.CreatedBy == null)
            .Include(p => p.Exercises)
            .AsNoTracking()
            .ToListAsync(ct);

        return plans;
    }

    public async Task<PlanEntity?> GetAsync(Guid planId, Guid? userId, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == planId, ct);
        
       return plan;
    }

    public async Task<List<PlanEntity>> GetAsync(List<Guid> planIds, CancellationToken ct)
    {
        var plans = await _context.Plans
            .Where(p => planIds.Contains(p.Id))
            .AsNoTracking()
            .ToListAsync(ct);

        return plans;
    } 
    
    public async Task<Guid> UpdateAsync(Guid id, List<Guid> exerciseIds, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        
        if (plan is null)
        {
            throw new InvalidOperationException("Plan not found");
        }
        
        var exercises = await _context.Exercises
            .Where(e => exerciseIds.Contains(e.Id))
            .ToListAsync(ct);

        plan.Exercises = exercises;

        await _context.SaveChangesAsync(ct);

        return plan.Id;
    }

    public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct)
    {
        var plan = await _context.Plans
            .Include(p => p.Exercises)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        
        if (plan is null)
        {
            throw new InvalidOperationException("Plan not found");
        }
        
        _context.Plans.Remove(plan);
        await _context.SaveChangesAsync(ct);

        return id;
    }

    /*public async Task<(int totalCount, List<PlanEntity> plans)> Search(string query, 
        PlanParameters planParameters, List<Guid> filter)
    {
        var connection = _context.Database.GetDbConnection();
        var offset = (planParameters.PageNumber - 1) * planParameters.PageSize;

        string sql = @"
        SELECT ""Id"", ""Category"", ""Exercises"", ""CreatedBy"" AS ""CreatedBy"",
               ts_rank(
                   to_tsvector('russian', coalesce(""Category"", '')) ||
                   to_tsvector('russian', coalesce(""Exercises""::text, '')),
                   plainto_tsquery('russian', @query)
               ) AS ""Rank""
        FROM ""Plans""
        WHERE 
            (
                to_tsvector('russian', coalesce(""Category"", '')) ||
                to_tsvector('russian', coalesce(""Exercises""::text, '')) 
            ) @@ plainto_tsquery('russian', @query)
            AND (""Id"" = ANY(@filter))
        ORDER BY ""Rank"" DESC
        LIMIT @limit OFFSET @offset;";

        var countSql = @"
        SELECT COUNT(*) 
        FROM ""Plans""
        WHERE
            to_tsvector('russian', coalesce(""Category"", '')) ||
            to_tsvector('russian', coalesce(""Exercises""::text, '')) @@
            plainto_tsquery('russian', @query);";

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        var plansRaw = await connection.QueryAsync<PlanSearchResultEntity>(sql, new
        {
            query,
            filter,
            limit = planParameters.PageSize,
            offset
        });

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, new { query });
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var plans = plansRaw
            .Select(r =>
            {
                var exercises = JsonSerializer.Deserialize<List<ExerciseEntity>>(r.Exercises, options) ?? new();
                var (plan, _) = PlanEntity.Create(r.Id, r.Category, exercises, r.CreatedBy);
                return plan;
            })
            .Where(p => p is not null)
            .ToList();

        return (totalCount, plans);
    }
    
    public async Task<(int totalCount, List<PlanEntity> plans)> Search(string query, 
        PlanParameters planParameters, Guid? filter)
    {
        var connection = _context.Database.GetDbConnection();
        var offset = (planParameters.PageNumber - 1) * planParameters.PageSize;

        string sql = @"
        SELECT ""Id"", ""Category"", ""Exercises"", ""CreatedBy"" AS ""CreatedBy"",
               ts_rank(
                   to_tsvector('russian', coalesce(""Category"", '')) ||
                   to_tsvector('russian', coalesce(""Exercises""::text, '')),
                   plainto_tsquery('russian', @query)
               ) AS ""Rank""
        FROM ""Plans""
        WHERE 
            (
                to_tsvector('russian', coalesce(""Category"", '')) ||
                to_tsvector('russian', coalesce(""Exercises""::text, '')) 
            ) @@ plainto_tsquery('russian', @query)
            AND (@filter IS NULL OR ""CreatedBy"" = @filter)
        ORDER BY ""Rank"" DESC
        LIMIT @limit OFFSET @offset;";

        var countSql = @"
        SELECT COUNT(*) 
        FROM ""Plans""
        WHERE
            to_tsvector('russian', coalesce(""Category"", '')) ||
            to_tsvector('russian', coalesce(""Exercises""::text, '')) @@
            plainto_tsquery('russian', @query);";

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        var plansRaw = await connection.QueryAsync<PlanSearchResultEntity>(sql, new
        {
            query,
            filter,
            limit = planParameters.PageSize,
            offset
        });

        var totalCount = await connection.ExecuteScalarAsync<int>(countSql, new { query });
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var plans = plansRaw
            .Select(r =>
            {
                var exercises = JsonSerializer.Deserialize<List<ExerciseEntity>>(r.Exercises, options) ?? new();
                var (plan, _) = PlanEntity.Create(r.Id, r.Category, exercises, r.CreatedBy);
                return plan;
            })
            .Where(p => p is not null)
            .ToList();

        return (totalCount, plans);
    }*/

}