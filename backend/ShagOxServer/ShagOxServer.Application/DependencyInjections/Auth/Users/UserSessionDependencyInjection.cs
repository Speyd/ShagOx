using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Create;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Query;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Refresh;
using ShagOxServer.Application.Services.Auth.Users.UserSessions.Validator;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Domain.Cache.Auth.User;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserSessionDependencyInjection
{
    public static IServiceCollection AddUserSessionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserSessionQueryService,
            UserSessionQueryService>();

        services.AddScoped<IUserSessionCreateService,
            UserSessionCreateService>();

        services.AddScoped<IUserSessionRefreshService,
           UserSessionRefreshService>();

        services.AddScoped<
            ICacheInvalidationService<UserSession, UserSessionCacheContext>,
            UserSessionInvalidationService>();

        services.AddScoped<UserSessionInvalidationService>();

        services.AddScoped<UserSessionValidator>();

        return services;
    }
}
