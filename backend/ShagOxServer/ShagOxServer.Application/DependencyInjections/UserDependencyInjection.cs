using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Interfaces.Services.UserRoles;
using ShagOxServer.Application.Interfaces.Services.Users.Delete;
using ShagOxServer.Application.Interfaces.Services.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Users.Update;
using ShagOxServer.Application.Services.UserRoles;
using ShagOxServer.Application.Services.Users.Delete;
using ShagOxServer.Application.Services.Users.Query;
using ShagOxServer.Application.Services.Users.Update;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.DependencyInjection;
public static class UserDependencyInjection
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();
        services.AddScoped<IUserDeleteService, UserDeleteService>();

        services.AddScoped<IUserRoleService, UserRoleService>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}