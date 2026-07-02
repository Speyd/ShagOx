using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Auth;
using ShagOxServer.Infrastructure.Interfaces.Auth.Roles;
using ShagOxServer.Infrastructure.Interfaces.Auth.Users;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Roles;
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
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        return services;
    }
}