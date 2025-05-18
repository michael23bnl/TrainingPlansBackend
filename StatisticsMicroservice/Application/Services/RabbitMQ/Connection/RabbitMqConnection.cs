using RabbitMQ.Client;

namespace StatisticsMicroservice.Services.RabbitMQ.Connection;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;
    
    public IConnection Connection => _connection!;
    
    public RabbitMqConnection()
    {
        InitializeConnection();
    }

    private async void InitializeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost",
        };

        _connection = await factory.CreateConnectionAsync();
    }

    public void Dispose()
    {
        _connection?.Dispose();
    }
}