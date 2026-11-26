using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Devices.Create;
using ShagOxServer.Application.Services.Devices.Create;
using ShagOxServer.Application.Services.Devices.Validator;

namespace ShagOxServer.Application.DependencyInjections.Devices;

public static class DeviceDependencyInjection
{
    public static IServiceCollection AddDeviceApplication(
            this IServiceCollection services)
    {
        //services.AddScoped<IUserDeviceQueryService,
        //    UserDeviceQueryService>();

        services.AddScoped<IDeviceCreateService,
            DeviceCreateService>();
  
        //services.AddScoped<
        //    ICacheInvalidationService<UserDevice, UserDeviceCacheContext>,
        //    UserDeviceInvalidationService>();

        services.AddScoped<DeviceValidator>();

        return services;
    }

}
