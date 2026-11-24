using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.UserSessions.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth.Users;
public static class UserSessionDependencyInjection
{
    public static IServiceCollection AddUserSessionInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserSessionQueryRepository,
            UserSessionQueryRepository>();

        services.AddScoped<IUserSessionExistsRepository,
            UserSessionExistsRepository>();

        return services;
    }
}
