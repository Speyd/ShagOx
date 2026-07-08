using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Roles.Create;
using ShagOxServer.Application.Interfaces.Services.Roles.Delete;
using ShagOxServer.Application.Interfaces.Services.Roles.Query;
using ShagOxServer.Application.Interfaces.Services.Roles.Update;
using ShagOxServer.Application.Services.Roles.Create;
using ShagOxServer.Application.Services.Roles.Delete;
using ShagOxServer.Application.Services.Roles.Query;
using ShagOxServer.Application.Services.Roles.Update;

namespace ShagOxServer.Application.DependencyInjection;
public static class RoleDependencyInjection
{
    public static IServiceCollection AddRoles(this IServiceCollection services)
    {
        services.AddScoped<IRoleQueryService, RoleQueryService>();
        services.AddScoped<IRoleCreateService, RoleCreateService>();
        services.AddScoped<IRoleUpdateService, RoleUpdateService>();
        services.AddScoped<IRoleDeleteService, RoleDeleteService>();


        return services;
    }
}