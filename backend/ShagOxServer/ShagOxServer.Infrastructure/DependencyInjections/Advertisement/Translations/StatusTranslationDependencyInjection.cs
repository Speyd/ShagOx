using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Statuses.Translations;
using ShagOxServer.Infrastructure.Persistence.Repositories.Advertisements.Statuses.Translations;

namespace ShagOxServer.Infrastructure.DependencyInjections.Advertisement.Translations;

public static class StatusTranslationDependencyInjection
{
    public static IServiceCollection AddStatusTranslationInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusTranslationQueryRepository,
            StatusTranslationQueryRepository>();

        services.AddScoped<IStatusTranslationExistsRepository, 
            StatusTranslationExistsRepository>();

        return services;
    }
}