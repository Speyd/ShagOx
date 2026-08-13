using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Create;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Delete;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Query;
using ShagOxServer.Application.Services.Advertisements.Statuses.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Advertisement.Translations;
public static class StatusTranslationDependencyInjection
{
    public static IServiceCollection AddStatusTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IStatusTranslationQueryService, 
            StatusTranslationQueryService>();

        services.AddScoped<IStatusTranslationCreateService,
            StatusTranslationCreateService>();

        services.AddScoped<IStatusTranslationDeleteService,
            StatusTranslationDeleteService>();

        services.AddScoped<StatusTranslationValidator>();

        return services;
    }
}