using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Services.Auth.UserRoles.Query;
using ShagOxServer.Application.Services.Auth.Users.Create;
using ShagOxServer.Application.Services.Auth.Users.Delete;
using ShagOxServer.Application.Services.Auth.Users.Query;
using ShagOxServer.Application.Services.Auth.Users.Update;
using ShagOxServer.Application.Services.Auth.Users.Update.Validator;
using ShagOxServer.Application.Services.Auth.Users.Validator;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.DependencyInjections.Auth;
public static class UserDependencyInjection
{
    public static IServiceCollection AddUsersApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();
        services.AddScoped<IUserDeleteService, UserDeleteService>();

        services.AddScoped<IUserRoleQueryService, UserRoleQueryService>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<UserValidator>();
        services.AddScoped<UserUpdateValidator>();

        services.AddScoped<UserCreater>();
        services.AddScoped<IUserRoleQueryService, UserRoleQueryService>();

        return services;
    }
}