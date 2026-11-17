using System.Text.Json;
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
    private readonly CurrencyRateStore _store;
    private readonly string _cacheFilePath;

    public CurrencyBackgroundService(
        IServiceScopeFactory scopeFactory,
        ILogger<CurrencyBackgroundService> logger,
        IOptions<CurrencyRatesUpdaterSettings> settings,
        CurrencyRateStore store,
        IHostEnvironment environment)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _settings = settings.Value;
        _store = store;
        _cacheFilePath = Path.GetFullPath(
            _settings.CacheFilePath,
            environment.ContentRootPath);
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        await LoadCacheAsync(stoppingToken);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var delay = GetDelayUntilNextUpdate();

                if (delay > TimeSpan.Zero)
                    await Task.Delay(delay, stoppingToken);

                var updated = await UpdateRatesAsync(stoppingToken);

                if (!updated)
                    await Task.Delay(_settings.RetryInterval, stoppingToken);
            }
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation(
                "Currency background service stopped.");
        }
    }

    private TimeSpan GetDelayUntilNextUpdate()
    {
        var snapshot = _store.Snapshot;

        if (snapshot is null)
            return TimeSpan.Zero;

        var nextUpdate = snapshot.UpdatedAt + _settings.UpdateInterval;

        return nextUpdate - DateTimeOffset.UtcNow;
    }

    private async Task<bool> UpdateRatesAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();

            var fxApi = scope.ServiceProvider
                .GetRequiredService<IFxApiService>();

            var response = await fxApi.GetLatestRatesAsync(
                "EUR",
                cancellationToken);

            var snapshot = new CurrencyRatesSnapshot(
                response.Data.ToDictionary(
                    x => x.Key,
                    x => x.Value.Value),
                DateTimeOffset.UtcNow);

            _store.Update(snapshot);

            await SaveCacheAsync(snapshot, cancellationToken);

            _logger.LogInformation(
                "Currency rates successfully updated at {Time}",
                snapshot.UpdatedAt);

            return true;
        }
        catch (OperationCanceledException)
            when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to update currency rates. Next attempt in {RetryInterval}",
                _settings.RetryInterval);

            return false;
        }
    }

    private async Task LoadCacheAsync(
        CancellationToken cancellationToken)
    {
        if (!File.Exists(_cacheFilePath))
            return;

        try
        {
            await using var stream = File.OpenRead(_cacheFilePath);

            var snapshot = await JsonSerializer
                .DeserializeAsync<CurrencyRatesSnapshot>(
                    stream,
                    cancellationToken: cancellationToken);

            if (snapshot is null || snapshot.Rates.Count == 0)
                return;

            _store.Update(snapshot);

            _logger.LogInformation(
                "Currency rates loaded from cache (updated at {Time})",
                snapshot.UpdatedAt);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(
                ex,
                "Failed to load currency rates cache from {Path}",
                _cacheFilePath);
        }
    }

    private async Task SaveCacheAsync(
        CurrencyRatesSnapshot snapshot,
        CancellationToken cancellationToken)
    {
        try
        {
            var directory = Path.GetDirectoryName(_cacheFilePath);

            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            await using var stream = File.Create(_cacheFilePath);

            await JsonSerializer.SerializeAsync(
                stream,
                snapshot,
                cancellationToken: cancellationToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(
                ex,
                "Failed to save currency rates cache to {Path}",
                _cacheFilePath);
        }
    }
}
