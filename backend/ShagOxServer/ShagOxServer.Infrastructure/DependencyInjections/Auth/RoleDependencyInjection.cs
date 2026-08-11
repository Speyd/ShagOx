using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth;
public static class RoleDependencyInjection
{
    public static IServiceCollection AddRoleInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();
        services.AddScoped<IRoleExistsRepository, RoleExistsRepository>();

        return services;
    }
}