using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Create;
using ShagOxServer.Application.Interfaces.Services.Devices.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Devices.Management;
using ShagOxServer.Application.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Services.Caches.Invalidations.Deliveries;
using ShagOxServer.Application.Services.Caches.Invalidations.Devices;
using ShagOxServer.Application.Services.Devices.Core.Create;
using ShagOxServer.Application.Services.Devices.Core.Query;
using ShagOxServer.Application.Services.Devices.Core.Validator;
using ShagOxServer.Application.Services.Devices.Management;
using ShagOxServer.Domain.Cache.Devices;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.DependencyInjections.Devices;
public static class DeviceDependencyInjection
{
    public static IServiceCollection AddDeviceApplication(
            this IServiceCollection services)
    {
        services.AddScoped<IDeviceQueryService,
            DeviceQueryService>();

        services.AddScoped<IDeviceCreateService,
            DeviceCreateService>();
  
        services.AddScoped<
            ICacheInvalidationService<Device, DeviceCacheContext>,
            DeviceInvalidationService>();

        services.AddScoped<DeviceInvalidationService>();

        services.AddScoped<IDeviceModerationService,
            DeviceModerationService>();

        services.AddScoped<DeviceValidator>();

        return services;
    }

}
