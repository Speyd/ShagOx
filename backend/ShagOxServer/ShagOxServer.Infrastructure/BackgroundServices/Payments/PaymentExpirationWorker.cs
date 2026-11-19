using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Systems.BackgroundServices;
using ShagOxServer.Application.Interfaces.Db;
using ShagOxServer.Application.Interfaces.Services.Payments;
using ShagOxServer.Domain.Entities.Payments.Enum;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Primary;

namespace ShagOxServer.Infrastructure.BackgroundServices.Payments;
public class PaymentExpirationWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly PaymentExpirationWorkerOptions _options;
    private readonly IPostgresLoadBalancer _loadBalancer;
    private readonly ILogger<PaymentExpirationWorker> _logger;

    private readonly int CountPending;


    public PaymentExpirationWorker(
        IServiceScopeFactory scopeFactory,
        IOptions<PaymentExpirationWorkerOptions> options,
        IPostgresLoadBalancer loadBalancer,
        ILogger<PaymentExpirationWorker> logger)
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
            "Payment expiration worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
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

                await using var scope =
                    _scopeFactory.CreateAsyncScope();

                var db = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                var paymentProcessing = scope.ServiceProvider
                    .GetRequiredService<IPaymentProcessingService>();

                var now = DateTime.UtcNow;

                var payments = await db.Payments
                    .Where(x =>
                        x.ExpiresAt <= now && x.Status == PaymentStatus.Processing)
                    .OrderBy(x => x.ExpiresAt)
                    .Take(CountPending)
                    .ToListAsync(stoppingToken);

                foreach (var payment in payments)
                {
                    await paymentProcessing.ProcessFailedPayment(
                        payment,
                        stoppingToken);
                }
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
                    "Payment expiration processing failed.");
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
            "Payment expiration worker stopped.");
    }
}
