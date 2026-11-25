using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.DependencyInjections.Specification.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
using ShagOxServer.Application.Services.Specification.Conditions.Create;
using ShagOxServer.Application.Services.Specification.Conditions.Delete;
using ShagOxServer.Application.Services.Specification.Conditions.Query;
using ShagOxServer.Application.Services.Specification.Conditions.Update;
using ShagOxServer.Application.Services.Specification.Conditions.Validator;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class ConditionDependencyInjection
{
    public static IServiceCollection AddConditionApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IConditionQueryService, 
            ConditionQueryService>();

        services.AddScoped<IConditionCreateService,
            ConditionCreateService>();

        services.AddScoped<IConditionUpdateService,
            ConditionUpdateService>();

        services.AddScoped<IConditionDeleteService,
            ConditionDeleteService>();

        services.AddScoped<ConditionValidator>();

        services.AddScoped<
            ICacheInvalidationService<Condition, ConditionCacheContext>,
            ConditionInvalidationService>();

        services.AddConditionTranslationApplication();

        return services;
    }
}
