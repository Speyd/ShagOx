using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management;
using ShagOxServer.Application.Services.Auth.Users.Management;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserManagementDependencyInjection
{
    public static IServiceCollection AddUserManagementApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserTrastManagementService,
            UserTrastManagementService>();

        services.AddScoped<IUserModerationService,
           UserModerationService>();

        return services;
    }
}
