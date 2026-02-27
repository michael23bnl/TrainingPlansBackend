
using Elastic.Clients.Elasticsearch;

namespace TrainingPlans.Infrastructure.Elasticsearch.ElasticClient;

public interface IElasticClientProvider
{
    ElasticsearchClient Client { get; }
    ElasticSettings Settings { get; }
}