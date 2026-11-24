using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Auth.Users.Statistics;
using ShagOxServer.Application.Services.Auth.Users.Statistics;

namespace ShagOxServer.Application.DependencyInjections.Auth.Users;
public static class UserStatisticsDependencyInjection
{
    public static IServiceCollection AddUserStatisticsApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserStatisticsService, UserStatisticsService>();

        return services;
    }
}
