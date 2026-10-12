using Docker.DotNet;
using Docker.DotNet.Models;

namespace ShagOxServer.Infrastructure.Monitoring;
public sealed class DockerCpuReader
{
    private readonly DockerClient _docker;

    public DockerCpuReader(
        DockerClient docker)
    {
        _docker = docker;
    }

    public async Task<float?> GetCpuAsync(
        string containerName,
        CancellationToken cancellationToken = default)
    {
        ContainerStatsResponse? stats = null;

        var progress = new SyncProgress<ContainerStatsResponse>(
            result => stats = result
        );

        await _docker.Containers.GetContainerStatsAsync(
            containerName,
            new ContainerStatsParameters
            {
                Stream = false,
                OneShot = false
            },
            progress,
            cancellationToken
        );

        if (stats?.CPUStats?.CPUUsage is null ||
            stats.PreCPUStats?.CPUUsage is null)
        {
            return null;
        }

        var cpuCurrent = stats.CPUStats.CPUUsage.TotalUsage;
        var cpuPrevious = stats.PreCPUStats.CPUUsage.TotalUsage;

        var systemCurrent = stats.CPUStats.SystemUsage;
        var systemPrevious = stats.PreCPUStats.SystemUsage;

        if (cpuCurrent < cpuPrevious ||
            systemCurrent <= systemPrevious)
        {
            return null;
        }

        var cpuCount = stats.CPUStats.OnlineCPUs;

        if (cpuCount == 0)
        {
            cpuCount = (uint)(
                stats.CPUStats.CPUUsage.PercpuUsage?.Count ?? 0
            );
        }

        if (cpuCount == 0)
            return null;

        var cpuDelta = (double)(cpuCurrent - cpuPrevious);
        var systemDelta = (double)(systemCurrent - systemPrevious);

        return (float)(
            cpuDelta / systemDelta * cpuCount * 100.0
        );
    }
}
