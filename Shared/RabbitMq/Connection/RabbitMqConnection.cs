using RabbitMQ.Client;

namespace Shared.RabbitMq.Connection;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private readonly IConnection? _connection;

    public IConnection Connection => _connection!;

    private RabbitMqConnection(IConnection connection)
    {
        _connection = connection;
    }

    public static async Task<RabbitMqConnection> InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };
        var connection = await factory.CreateConnectionAsync();
        
        return new RabbitMqConnection(connection);
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}