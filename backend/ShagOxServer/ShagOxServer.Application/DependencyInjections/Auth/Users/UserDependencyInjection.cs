using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserDependencyInjection
{
    public static IServiceCollection AddUsersApplication(
        this IServiceCollection services)
    {
        services.AddUserCoreApplication();
        services.AddUserManagementApplication();
        services.AddUserStatisticsApplication();
        services.AddUserRolesApplication();

        services.AddUserDeviceApplication();
        services.AddUserSessionApplication();

        services.AddUserTwoFactorApplication();

        return services;
    }
}
