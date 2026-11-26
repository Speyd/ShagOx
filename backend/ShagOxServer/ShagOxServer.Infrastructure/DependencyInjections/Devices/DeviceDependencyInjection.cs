using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Devices;
using ShagOxServer.Application.Interfaces.Repositories.Devices.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Devices;
using ShagOxServer.Infrastructure.Persistence.Repositories.Devices.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Devices;
public static class DeviceDependencyInjection
{
    public static IServiceCollection AddDeviceInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IDeviceQueryRepository,
            DeviceQueryRepository>();

        services.AddScoped<IDeviceExistsRepository,
            DeviceExistsRepository>();

        services.AddScoped<IDeviceCommandsRepository,
            DeviceCommandsRepository>();

        return services;
    }
}
