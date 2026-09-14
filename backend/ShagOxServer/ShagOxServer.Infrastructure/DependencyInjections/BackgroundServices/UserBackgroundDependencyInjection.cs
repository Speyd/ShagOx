using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.BackgroundServices;

namespace ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
public static class UserBackgroundDependencyInjection
{
    public static IServiceCollection AddUserBackgroundInfrastructure(
        this IServiceCollection services)
    {
        services.AddHostedService<PendingUserCleanupService>();

        return services;
    }
}