using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Create.Validator;
using ShagOxServer.Application.Services.Specification.Currencies.Delete;
using ShagOxServer.Application.Services.Specification.Currencies.Query;
using ShagOxServer.Application.Services.Specification.Currencies.Update;
using ShagOxServer.Application.Services.Specification.Currencies.Validator;

namespace ShagOxServer.Application.DependencyInjections.Specification;
public static class CurrencyDependencyInjection
{
    public static IServiceCollection AddCurrencyApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICurrencyQueryService, CurrencyQueryService>();
        services.AddScoped<ICurrencyUpdateService, CurrencyUpdateService>();
        services.AddScoped<ICurrencyDeleteService, CurrencyDeleteService>();

        services.AddScoped<CurrencyCreateValidator>();
        services.AddScoped<CurrencyValidator>();

        return services;
    }
}