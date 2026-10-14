using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using ShagOxServer.Application.Common.Settings.Db;
using ShagOxServer.Domain.Entities.Db;
using ShagOxServer.Infrastructure.Monitoring;

namespace ShagOxServer.Infrastructure.BackgroundServices;
public class ReplicaHealthCheckeService
    : BackgroundService
{
    private readonly ILogger<ReplicaHealthCheckeService> _logger;

    private readonly DatabaseHealthCheckSettings _setting;

    private readonly IReadOnlyList<Replica> _replicas;

    private readonly DockerCpuReader _dockerCpuReader;


    public ReplicaHealthCheckeService(
        IOptions<DatabaseHealthCheckSettings> setting,
        ILogger<ReplicaHealthCheckeService> logger,
        IReadOnlyList<Replica> replicas,
        DockerCpuReader dockerCpuReader)
    {
        _setting = setting.Value;
        _logger = logger;
        _replicas = replicas;
        _dockerCpuReader = dockerCpuReader;
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
                _setting.CheckInterval,
                stoppingToken
            );
        }
    }

    private async Task CheckReplicaAsync(
        Replica replica,
        CancellationToken stoppingToken)
    {
        try
        {
            LogReplicaInfo(replica);

            using var timeoutCts = CancellationTokenSource
                .CreateLinkedTokenSource(stoppingToken);

            timeoutCts.CancelAfter(_setting.CheckTimeout);

            await using var connection =
                new NpgsqlConnection(replica.ConnectionString);

            await connection.OpenAsync(timeoutCts.Token);

            await using var command = connection.CreateCommand();

            command.CommandText = "SELECT 1";
            command.CommandTimeout = _setting.CommandTimeoutSeconds;

            await command.ExecuteScalarAsync(timeoutCts.Token);

            replica.IsHealthy = true;
            replica.LastHealthCheck = DateTime.UtcNow;

            try
            {
                var cpu = await _dockerCpuReader.GetCpuAsync(
                    replica.ContainerName,
                    timeoutCts.Token);

                if (cpu.HasValue)
                    replica.Cpu = cpu.Value;
            }
            catch (OperationCanceledException)
                when (stoppingToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(
                    ex,
                    "Failed to read CPU stats for replica on port {Port}",
                    GetPort(replica.ConnectionString));
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
            replica.IsHealthy = false;

            _logger.LogWarning(
                ex,
                "Replica(Port:{Port}) health check failed",
                GetPort(replica.ConnectionString));
        }
    }

    private void LogReplicaInfo(
        Replica replica)
    {
        _logger.LogInformation(
            """
        Replica status:
          Container: {ContainerName}
          Port: {Port}
          Healthy: {IsHealthy}
          CPU: {Cpu:F2}
          Active connections: {ActiveConnections}
          Last health check: {LastHealthCheck:O}
        """,
            replica.ContainerName,
            GetPort(replica.ConnectionString),
            replica.IsHealthy,
            replica.Cpu,
            replica.ActiveConnections,
            replica.LastHealthCheck);
    }
    private static int GetPort(
        string connectionString)
    {
        try
        {
            return new NpgsqlConnectionStringBuilder(
                connectionString).Port;
        }
        catch
        {
            return -1;
        }
    }
}
