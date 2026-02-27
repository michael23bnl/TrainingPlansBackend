
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Options;

namespace TrainingPlans.Infrastructure.Elasticsearch.ElasticClient;

public class ElasticClientProvider : IElasticClientProvider
{
    public ElasticsearchClient Client { get; }
    public ElasticSettings Settings { get; }

    public ElasticClientProvider(IOptions<ElasticSettings> elasticSettings)
    {
        Settings = elasticSettings.Value;
        var clientSettings = new ElasticsearchClientSettings(new Uri(Settings.Url))
            .DefaultFieldNameInferrer(p => p)
            .DefaultIndex(Settings.DefaultIndex);
        
        Client =  new ElasticsearchClient(clientSettings);
    }
}