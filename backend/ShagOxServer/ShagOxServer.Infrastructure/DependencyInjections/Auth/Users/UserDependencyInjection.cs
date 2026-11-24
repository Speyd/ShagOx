using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Core;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Statistics;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Core;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Query;
using ShagOxServer.Infrastructure.Persistence.Repositories.Auth.Users.Statistics;

namespace ShagOxServer.Infrastructure.DependencyInjections.Auth.Users;
public static class UserDependencyInjection
{
    public static IServiceCollection AddUserInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUserQueryRepository,
            UserQueryRepository>();

        services.AddScoped<IUserExistsRepository,
            UserExistsRepository>();

        services.AddScoped<IUserStatisticsRepository,
            UserStatisticsRepository>();

        services.AddUserDeviceInfrastructure();

        services.AddUserSessionInfrastructure();

        return services;
    }
}
