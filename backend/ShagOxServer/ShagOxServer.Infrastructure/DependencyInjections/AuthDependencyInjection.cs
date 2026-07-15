using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Roles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.UserRoles;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.UserRoles;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class AuthDependencyInjection
{
    public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services)
    {
        // User
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserQueryRepository, UserQueryRepository>();
        services.AddScoped<IUserExistsRepository, UserExistsRepository>();


        // Role
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleQueryRepository, RoleQueryRepository>();
        services.AddScoped<IRoleExistsRepository, RoleExistsRepository>();

        // UserRole
        services.AddScoped<IUserRoleQueryRepository, UserRoleQueryRepository>();
        services.AddScoped<IUserRoleExistsRepository, UserRoleExistsRepository>();


        return services;
    }
}