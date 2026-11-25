using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Auth.Roles.Create;
using ShagOxServer.Application.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Services.Auth.Roles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Update;
using ShagOxServer.Application.Services.Auth.Roles.Validator;
using ShagOxServer.Application.Services.Auth.Users.Roles;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Domain.Cache.Auth;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class RoleDependencyInjection
{
    public static IServiceCollection AddRoleApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IRoleCreateService, RoleCreateService>();
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IRoleUpdateService, RoleUpdateService>();
        services.AddScoped<IRoleDeleteService, RoleDeleteService>();

        services.AddScoped<
            ICacheInvalidationService<Role, RoleCacheContext>,
            RoleInvalidationService>();

        services.AddScoped<RoleValidator>();

        services.AddScoped<UserRoleService>();

        return services;
    }
}
