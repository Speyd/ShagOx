using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Verifications;
public static class VerificationDependencyInjection
{
    public static IServiceCollection AddVerificationApplication(
        this IServiceCollection services)
    {
        services.AddVerificationCodeApplication();

        return services;
    }
}