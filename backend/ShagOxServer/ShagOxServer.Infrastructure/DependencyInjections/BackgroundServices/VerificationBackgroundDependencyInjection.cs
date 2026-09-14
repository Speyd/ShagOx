using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Api.BackgroundServices;
namespace ShagOxServer.Infrastructure.DependencyInjections.BackgroundServices;
public static class VerificationBackgroundDependencyInjection
{
    public static IServiceCollection AddVerificationBackgroundInfrastructure(
        this IServiceCollection services)
    {
        services.AddHostedService<VerificationCodeCleanupService>();

        return services;
    }
}