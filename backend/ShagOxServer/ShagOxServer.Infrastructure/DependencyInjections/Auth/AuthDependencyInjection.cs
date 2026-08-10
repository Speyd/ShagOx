using Microsoft.Extensions.DependencyInjection;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services)
    {
        services.AddUserInfrastructure();

        services.AddRoleInfrastructure();

        services.AddUserRoleInfrastructure();
        
        return services;
    }
}