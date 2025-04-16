
using RabbitMQ.Client;

namespace ApiGateway.Services.RabbitMq.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}