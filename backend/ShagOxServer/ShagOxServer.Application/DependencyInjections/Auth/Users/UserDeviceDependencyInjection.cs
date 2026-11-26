using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Auth.Users.UserDevices;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Query;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserDeviceDependencyInjection
{
    public static IServiceCollection AddUserDeviceApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserDeviceQueryService,
            UserDeviceQueryService>();

        services.AddScoped<IUserDeviceCreateService,
            UserDeviceCreateService>();

        services.AddScoped<IUserDeviceAuthenticationService,
            UserDeviceAuthenticationService>();

        services.AddScoped<
            ICacheInvalidationService<UserDevice, UserDeviceCacheContext>,
            UserDeviceInvalidationService>();

        services.AddScoped<UserDeviceValidator>();  

        return services;
    }
}
