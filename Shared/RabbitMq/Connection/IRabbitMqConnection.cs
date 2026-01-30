
using RabbitMQ.Client;

namespace Shared.RabbitMq.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}