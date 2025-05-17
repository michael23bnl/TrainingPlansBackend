using RabbitMQ.Client;

namespace TrainingPlans.Infrastructure.RabbitMq.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}