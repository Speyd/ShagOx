using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Verifications;
public static class VerificationDependencyInjection
{
    public static IServiceCollection AddVerificationInfrastructure(
        this IServiceCollection services)
    {
        services.AddVerificationCodeInfrastructure();

        return services;
    }
}