using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
using ShagOxServer.Application.Interfaces.Db;
using ShagOxServer.Application.Interfaces.Services.Payments;

namespace ShagOxServer.Infrastructure.BackgroundServices.Payments;
public class PaymentWebhookWorker 
    : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly WebhookWorkerOptions _options;
    private readonly IPostgresLoadBalancer _loadBalancer;
    private readonly ILogger<PaymentWebhookWorker> _logger;

    private readonly int CountPending;


    public PaymentWebhookWorker(
        IServiceScopeFactory scopeFactory,
        IOptions<WebhookWorkerOptions> options,
        IPostgresLoadBalancer loadBalancer,
        ILogger<PaymentWebhookWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _options = options.Value;
        _loadBalancer = loadBalancer;
        _logger = logger;

        CountPending = options.Value.CountPending <= 0 ?
            100 : 
            options.Value.CountPending;
    }


    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "Payment webhook worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_loadBalancer.HasHealthyReplica())
            {
                _logger.LogWarning(
                    "No healthy PostgreSQL replicas. Waiting...");

                await Task.Delay(
                    _options.PendingInterval,
                    stoppingToken);

                continue;
            }

            try
            {
                await using var scope =
                    _scopeFactory.CreateAsyncScope();

                var processor = scope.ServiceProvider
                    .GetRequiredService<IPaymentWebhookProcessor>();

                await processor.ProcessPendingAsync(
                    CountPending,
                    stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Payment webhook processing failed.");
            }

            try
            {
                await Task.Delay(
                    _options.PendingInterval,
                    stoppingToken);
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
        }

        _logger.LogInformation(
            "Payment webhook worker stopped.");
    }
}
