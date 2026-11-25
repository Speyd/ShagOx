using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.UserRoles.Query;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserRolesDependencyInjection
{
    public static IServiceCollection AddUserRolesApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserRoleQueryService, UserRoleQueryService>();

        return services;
    }
}
