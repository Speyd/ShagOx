using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Validator;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserSessionDependencyInjection
{
    public static IServiceCollection AddUserSessionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserSessionCreateService,
            UserSessionCreateService>();

        services.AddScoped<IUserSessionRefreshService,
           UserSessionRefreshService>();

        services.AddScoped<UserSessionValidator>();

        return services;
    }
}
