using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using ShagOxServer.Application.Common.Db;
using ShagOxServer.Application.Common.Settings.Systems;

namespace ShagOxServer.Infrastructure.BackgroundServices;
public class ReplicaHealthCheckeService
    : BackgroundService
{
    private readonly ILogger<ReplicaHealthCheckeService> _logger;

    private readonly BackgroundServiceSettings _setting;

    private readonly IReadOnlyList<Replica> _replicas;

    private static readonly TimeSpan CheckTimeout =
        TimeSpan.FromSeconds(3);


    public ReplicaHealthCheckeService(
        IOptions<BackgroundServiceSettings> setting,
        ILogger<ReplicaHealthCheckeService> logger,
        IReadOnlyList<Replica> replicas)
    {
        _setting = setting.Value;
        _logger = logger;
        _replicas = replicas;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {

                foreach (var replica in _replicas)
                {
                    await CheckReplicaAsync(replica,
                        stoppingToken);
                }
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    "Replica health checke service stopped.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Replica health checke service failed.");
            }

            await Task.Delay(
                _setting.VerificationCodeCleanupInterval,
                stoppingToken
            );
        }
    }

    private async Task CheckReplicaAsync(
        Replica replica,
        CancellationToken stoppingToken)
    {
        int port = -1;

        try
        {
            using var timeoutCts = CancellationTokenSource
                .CreateLinkedTokenSource(stoppingToken);

            timeoutCts.CancelAfter(CheckTimeout);

            await using var connection =
                new NpgsqlConnection(replica.ConnectionString);

            port = connection.Port;

            await connection.OpenAsync(timeoutCts.Token);

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT 1";
            command.CommandTimeout = 3;

            await command.ExecuteScalarAsync(timeoutCts.Token);

            replica.IsHealthy = true;
        }
        catch (OperationCanceledException)
            when (stoppingToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception ex)
        {
            replica.IsHealthy = false;

            _logger.LogWarning(
                ex,
                $"Replica(port:{port}) health check failed");
        }
    }
}
