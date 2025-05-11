using System.Collections.Concurrent;
using System.Globalization;
using TrainingPlans.Models;

namespace TrainingPlans.Services.Statistics;

public class StatisticsService : IStatisticsService
{
    public ConcurrentDictionary<DateOnly, Dictionary<string, int>> GetStatistics(List<CompletedPlanModel> plans)
    {
        var statistics = new ConcurrentDictionary<DateOnly, Dictionary<string, int>>();
    
        Parallel.ForEach(plans, plan =>
        {
            var categoryList = plan.Plan.Category;
            if (string.IsNullOrEmpty(categoryList))
            {
                return;
            }
            
            var planCategories = categoryList
                .Split(',')
                .Select(c => c.Trim())
                .Select(c => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(c.ToLower()));
            
            var date = plan.CompletionDate;

            foreach (var category in planCategories)
            {
                statistics.AddOrUpdate(date, new Dictionary<string, int> { [category] = 1 },
                    (key, dict) =>
                    {
                        lock (dict)
                        {
                            if (dict.ContainsKey(category))
                                dict[category]++;
                            else
                                dict[category] = 1;
                            return dict;
                        }
                    });
                /*categories.AddOrUpdate(category, 1,
                    (key, oldValue) => oldValue + 1);*/
            }
        });
    
        return statistics;
    }
}