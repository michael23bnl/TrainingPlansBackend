
using RabbitMQ.Client;

namespace UserMicroservice.Services.RabbitMq.Connection;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}