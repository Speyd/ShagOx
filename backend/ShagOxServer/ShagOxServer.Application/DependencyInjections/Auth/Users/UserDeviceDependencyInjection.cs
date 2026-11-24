using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Create;
using ShagOxServer.Application.Services.Auth.Users.UserDevices.Validator;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserDeviceDependencyInjection
{
    public static IServiceCollection AddUserDeviceApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserDeviceCreateService,
            UserDeviceCreateService>();
        
        services.AddScoped<UserDeviceValidator>();   

        return services;
    }
}
