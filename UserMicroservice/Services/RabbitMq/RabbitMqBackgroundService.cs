namespace UserMicroservice.Services.RabbitMq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
public class RabbitMqBackgroundService : BackgroundService
{
    private readonly ILogger<RabbitMqBackgroundService> _logger;
    private readonly IServiceProvider _services;

    public RabbitMqBackgroundService(IServiceProvider services,
        ILogger<RabbitMqBackgroundService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await ReceiveMessage(stoppingToken);
        
    }

    private async Task ReceiveMessage(CancellationToken stoppingToken)
    {
        using (var scope = _services.CreateScope())
        {
            var scopedRabbitMqSubscriber = scope.ServiceProvider.GetRequiredService<IMessageSubscriber>();
            await scopedRabbitMqSubscriber.ReceiveMessage(stoppingToken);
        }
    }
}
