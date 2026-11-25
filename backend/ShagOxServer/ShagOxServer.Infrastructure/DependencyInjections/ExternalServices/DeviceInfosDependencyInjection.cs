using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Providers.User;
using ShagOxServer.Infrastructure.ExternalServices.Auth;

namespace ShagOxServer.Infrastructure.DependencyInjections.ExternalServices;
public static class DeviceInfosDependencyInjection
{
    public static IServiceCollection AddDeviceInfosInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IDeviceInfoProvider,
            DeviceInfoProvider>();

        return services;
    }
}
