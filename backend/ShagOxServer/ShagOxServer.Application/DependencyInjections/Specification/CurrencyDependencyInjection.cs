using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification;
using ShagOxServer.Application.Services.Specification.Currencies.Create;
using ShagOxServer.Application.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class CurrencyDependencyInjection
{
    public static IServiceCollection AddCurrencyApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICurrencyQueryService,
            CurrencyQueryService>();

        services.AddScoped<ICurrencyCreateService,
            CurrencyCreateService>();

        services.AddScoped<ICurrencyUpdateService,
            CurrencyUpdateService>();

        services.AddScoped<ICurrencyDeleteService,
            CurrencyDeleteService>();

        services.AddScoped<CurrencyValidator>();

        services.AddScoped<
            ICacheInvalidationService<Currency, CurrencyCacheContext>,
            CurrencyInvalidationService>();

        services.AddScoped<CurrencyInvalidationService>();

        return services;
    }
}
