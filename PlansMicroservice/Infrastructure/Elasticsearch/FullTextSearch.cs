using Elastic.Clients.Elasticsearch;
using Shared.Pagination;
using TrainingPlans.Application.Abstractions;
using TrainingPlans.Application.Models;
using TrainingPlans.Infrastructure.Elasticsearch.ElasticClient;
using TrainingPlans.Infrastructure.Elasticsearch.Models;

namespace TrainingPlans.Infrastructure.Elasticsearch;

public class FullTextSearch : IFullTextSearch
{
    private readonly ElasticsearchClient _client;
    private readonly ElasticSettings _settings;

    public FullTextSearch(IElasticClientProvider elasticClientProvider)
    {
        _client = elasticClientProvider.Client;
        _settings = elasticClientProvider.Settings;
    }

    public async Task<(int totalCount, List<PlanSearchResult> plans)>
        SearchAsync(string query, Guid? userId, PlanParameters parameters, CancellationToken ct)
    {
        var response = await _client.SearchAsync<PlanSearchDocument>(s => s
                .Index(_settings.DefaultIndex)
                .Query(q => q
                    .Bool(b =>
                    {
                        b.Filter(fb =>
                        {
                            if (userId.HasValue)
                            {
                                fb.Term(t => t
                                    .Field(f => f.UserId)
                                    .Value(userId.Value.ToString()));
                            }
                            else
                            {
                                fb.Bool(bb => bb
                                    .MustNot(mn => mn
                                        .Exists(e => e
                                            .Field(f => f.UserId))));
                            }
                        });
                        b.Should(
                            sh => sh.Match(m => m
                                .Field(f => f.Description)
                                .Query(query)
                                .Fuzziness(new Fuzziness("AUTO"))
                                .Boost(1)
                            ),
                            sh => sh.Match(m => m
                                .Field(f => f.Tags)
                                .Query(query)
                                .Fuzziness(new Fuzziness("AUTO"))
                                .Boost(2)
                            ),
                            sh => sh.Nested(n => n
                                .Path(p => p.Exercises)
                                .Query(nq => nq
                                    .Bool(nb => nb
                                        .Should(
                                            qd => qd.Match(m => m
                                                .Field("exercises.name"!)
                                                .Query(query)
                                                .Fuzziness(new Fuzziness("AUTO"))
                                                .Boost(3)
                                            ),
                                            qd => qd.Match(m => m
                                                .Field("exercises.description"!)
                                                .Query(query)
                                                .Fuzziness(new Fuzziness("AUTO"))
                                            )
                                        )
                                    )
                                )
                            )
                        );
                        b.MinimumShouldMatch(1);
                    })
                )
                .Sort(so => so
                    .Score(ss => ss.Order(SortOrder.Desc))
                    .Field(ps => ps.CreatedAt, fs => fs.Order(SortOrder.Desc)))
                .From((parameters.PageNumber - 1) * parameters.PageSize)
                .Size(parameters.PageSize),
            ct);
        
        var searchResult = response.Documents.ToList()
            .Select(p => new PlanSearchResult
            {
                Id = p.Id,
                Description = p.Description,
                Tags = p.Tags,
                Exercises = p.Exercises
                    .Select(e => new ExerciseSearchResult
                    {
                        Id = e.Id,
                        Name = e.Name,
                        MuscleGroup = e.MuscleGroup,
                        Description = e.Description,
                        Sets = e.Sets,
                        Reps = e.Reps,
                        Notes = e.Notes
                    }).ToList()
            }).ToList()
            ;
        return ((int)response.Total, searchResult);
    }
    
}