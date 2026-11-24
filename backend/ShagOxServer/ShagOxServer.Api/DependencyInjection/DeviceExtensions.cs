using ShagOxServer.Api.Providers;
using ShagOxServer.Application.Interfaces.Providers.User;

namespace ShagOxServer.Api.DependencyInjection;
public static class DeviceExtensions
{
    public static IServiceCollection AddDevice(
        this IServiceCollection services)
    {
        services.AddScoped<IDeviceProvider,
            DeviceProvider>();

        return services;
    }
}
