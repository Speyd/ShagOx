using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Common.Context;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Delete;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Core.Update;
using ShagOxServer.Application.Interfaces.Services.Common.Context;
using ShagOxServer.Application.Services.Auth.Users.Core.Create;
using ShagOxServer.Application.Services.Auth.Users.Core.Delete;
using ShagOxServer.Application.Services.Auth.Users.Core.Query;
using ShagOxServer.Application.Services.Auth.Users.Core.Validator;
using ShagOxServer.Application.Services.Auth.Users.Update;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserCoreDependencyInjection
{
    public static IServiceCollection AddUserCoreApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();

        services.AddScoped<IUserQueryService, UserQueryService>();
        services.AddScoped<IUserAdminQueryService, UserAdminQueryService>();
        services.AddScoped<IUserUpdateService, UserUpdateService>();
        services.AddScoped<IUserDeleteService, UserDeleteService>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddScoped<UserValidator>();
        services.AddScoped<UserCreater>();

        return services;
    }
}
