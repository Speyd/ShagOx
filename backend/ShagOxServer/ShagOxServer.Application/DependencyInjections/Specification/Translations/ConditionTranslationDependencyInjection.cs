using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Translations.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification.Translations;
public static class ConditionTranslationDependencyInjection
{
    public static IServiceCollection AddConditionTranslationApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionTranslationQueryService,
            ConditionTranslationQueryService>();

        services.AddScoped<IConditionTranslationCreateService,
            ConditionTranslationCreateService>();

        services.AddScoped<IConditionTranslationDeleteService,
            ConditionTranslationDeleteService>();

        services.AddScoped<IConditionTranslationUpdateService,
            ConditionTranslationUpdateService>();

        services.AddScoped<ConditionTranslationValidator>();

        return services;
    }
}