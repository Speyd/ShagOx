using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Roles.Update;
using ShagOxServer.Application.Services.Auth.Roles.Delete;
using ShagOxServer.Application.Services.Auth.Roles.Query;
using ShagOxServer.Application.Services.Auth.Roles.Update;
using ShagOxServer.Application.Services.Auth.Roles.Validator;

namespace ShagOxServer.Application.DependencyInjection;
public static class RoleDependencyInjection
{
    public static IServiceCollection AddRoles(this IServiceCollection services)
    {
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IRoleUpdateService, RoleUpdateService>();
        services.AddScoped<IRoleDeleteService, RoleDeleteService>();

        services.AddScoped<RoleValidator>();

        return services;
    }
}