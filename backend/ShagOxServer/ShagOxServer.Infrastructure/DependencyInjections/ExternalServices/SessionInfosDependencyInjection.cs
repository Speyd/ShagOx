using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Providers.Sessions;
using ShagOxServer.Infrastructure.ExternalServices.Auth;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class SessionInfosDependencyInjection
{
    public static IServiceCollection AddSessionInfosInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ISessionInfoProvider,
            SessionInfoProvider>();

        return services;
    }
}
