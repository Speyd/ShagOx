using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.DependencyInjections.Auth.Users;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(
        this IServiceCollection services)
    {
        services.AddUserInfrastructure();

        services.AddRoleInfrastructure();

        services.AddUserRoleInfrastructure();
        
        return services;
    }
}