using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserDevices.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserDevices.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth.Users;
public static class UserDeviceDependencyInjection
{
    public static IServiceCollection AddUserDeviceInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserDeviceQueryRepository,
            UserDeviceQueryRepository>();

        services.AddScoped<IUserDeviceExistsRepository,
            UserDeviceExistsRepository>();

        services.AddScoped<IUserDeviceCommandsRepository,
            UserDeviceCommandsRepository>();

        return services;
    }
}
