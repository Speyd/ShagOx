using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Interfaces.Providers.User;

namespace ShagOxServer.Api.DependencyInjection;
public static class SessionExtensions
{
    public static IServiceCollection AddUserSession(
        this IServiceCollection services)
    {
        services.AddScoped<ISessionProvider,
            SessionProvider>();

        return services;
    }
}
