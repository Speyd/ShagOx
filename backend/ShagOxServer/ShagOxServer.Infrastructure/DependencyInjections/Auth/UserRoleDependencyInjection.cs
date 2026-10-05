using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth;
public static class UserRoleDependencyInjection
{
    public static IServiceCollection AddUserRoleInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRoleQueryRepository, UserRoleQueryRepository>();
        services.AddScoped<IUserRoleExistsRepository, UserRoleExistsRepository>();

        return services;
    }
}