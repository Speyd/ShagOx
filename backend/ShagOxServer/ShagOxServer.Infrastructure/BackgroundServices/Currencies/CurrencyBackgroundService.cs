using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Currencies;
using ShagOxServer.Application.Interfaces.Services.Common.Currencies.FxApi;
using ShagOxServer.Infrastructure.ExternalServices.Currencies;

namespace ShagOxServer.Infrastructure.BackgroundServices.Currencies;
public class CurrencyBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<CurrencyBackgroundService> _logger;
    private readonly CurrencyRatesUpdaterSettings _settings;


    public CurrencyBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<CurrencyBackgroundService> logger,
        IOptions<CurrencyRatesUpdaterSettings> settings)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _settings = settings.Value;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(
            _settings.UpdateInterval);

        await UpdateRatesAsync(stoppingToken);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await UpdateRatesAsync(stoppingToken);
        }
    }

    private async Task UpdateRatesAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();

            var fxApi = scope.ServiceProvider
                .GetRequiredService<IFxApiService>();

            var store = scope.ServiceProvider
                .GetRequiredService<CurrencyRateStore>();

            var rates = await fxApi.GetLatestRatesAsync(
                "EUR",
                cancellationToken);

            store.Update(rates);

            _logger.LogInformation(
                "Currency rates successfully updated at {Time}",
                DateTimeOffset.UtcNow);
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Currency background service stopped.");
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update currency rates");
        }
    }
}
