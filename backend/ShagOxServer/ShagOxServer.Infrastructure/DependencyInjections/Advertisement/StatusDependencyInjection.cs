using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement;
public static class StatusDependencyInjection
{
    public static IServiceCollection AddStatusInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusQueryRepository, StatusQueryRepository>();
        services.AddScoped<IStatusExistsRepository, StatusExistsRepository>();

        return services;
    }
}