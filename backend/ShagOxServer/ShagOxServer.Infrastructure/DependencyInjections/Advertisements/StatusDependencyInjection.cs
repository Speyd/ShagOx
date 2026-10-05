using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses;
using ShagOxServer.Infrastructure.DependencyInjections.Advertisements.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Query;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisements;
public static class StatusDependencyInjection
{
    public static IServiceCollection AddStatusInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusQueryRepository, StatusQueryRepository>();
        services.AddScoped<IStatusExistsRepository, StatusExistsRepository>();

        services.AddStatusTranslationInfrastructure();

        return services;
    }
}