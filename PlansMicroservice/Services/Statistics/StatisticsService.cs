using System.Collections.Concurrent;
using System.Globalization;
using TrainingPlans.Models;

namespace TrainingPlans.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    public ConcurrentDictionary<string, int> GetStatistics(List<PlanModel> plans)
    {
        var categories = new ConcurrentDictionary<string, int>();
    
        Parallel.ForEach(plans, plan =>
        {
            var categoryList = plan.Category;
            if (string.IsNullOrEmpty(categoryList))
            {
                return;
            }
            
            var planCategories = categoryList.Split(',')
                .Select(c => c.Trim())
                .Select(c => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.ToLower()));

            foreach (var category in planCategories)
            {
                categories.AddOrUpdate(category, 1,
                    (key, oldValue) => oldValue + 1);
            }
        });
    
        return categories;
    }
}