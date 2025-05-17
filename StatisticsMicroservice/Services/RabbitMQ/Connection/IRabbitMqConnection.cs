using RabbitMQ.Client;

namespace StatisticsMicroservice.Services.RabbitMQ.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}