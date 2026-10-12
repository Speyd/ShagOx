using Docker.DotNet;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Monitoring;

namespace ShagOxServer.Infrastructure.DependencyInjections.Monitoring;
public static class MonitoringDependencyInjection
{
    public static IServiceCollection AddMonitoring(
        this IServiceCollection services)
    {
        services.AddSingleton<DockerClient>(_ =>
           new DockerClientConfiguration(
               new Uri("npipe://./pipe/docker_engine"))
               .CreateClient());

        services.AddSingleton<DockerCpuReader>();

        return services;
    }
}
