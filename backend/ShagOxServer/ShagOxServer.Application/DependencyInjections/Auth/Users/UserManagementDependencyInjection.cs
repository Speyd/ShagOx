using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Moderation;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Management.Trast;
using ShagOxServer.Application.Services.Auth.Users.Management.Moderation;
using ShagOxServer.Application.Services.Auth.Users.Management.Trast;

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
