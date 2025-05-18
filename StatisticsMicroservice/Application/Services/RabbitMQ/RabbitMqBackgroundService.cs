using StatisticsMicroservice.Services.Interfaces;

namespace StatisticsMicroservice.Services.RabbitMQ;

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
        var scopedServiceProvider = _services.CreateScope().ServiceProvider;
        var subscriber = scopedServiceProvider.GetRequiredService<IMessageSubscriber>();

        subscriber.ReceiveMessage(
            async message =>
            {
                using var scope = _services.CreateScope();
                var scopedStatisticsService = scope.ServiceProvider
                    .GetRequiredService<IStatisticsService>();
                await scopedStatisticsService.SetStatistics(message);
            },
            "statistics.create",
            stoppingToken);

        subscriber.ReceiveMessage(
            async message =>
            {
                using var scope = _services.CreateScope();
                var scopedStatisticsService = scope.ServiceProvider
                    .GetRequiredService<IStatisticsService>();
                await scopedStatisticsService.DeleteStatistics(message);
            },
            "statistics.delete",
            stoppingToken);

        await Task.CompletedTask;
    }
}