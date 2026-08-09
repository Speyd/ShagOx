using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationInfrastructure(this IServiceCollection services)
    {
        // Currency
        services.AddScoped<ICurrencyQueryRepository, CurrencyQueryRepository>();
        services.AddScoped<ICurrencyExistsRepository, CurrencyExistsRepository>();

        // Condition
        services.AddScoped<IConditionQueryRepository, ConditionQueryRepository>();
        services.AddScoped<IConditionExistsRepository, ConditionExistsRepository>();

        // Image
        services.AddScoped<IImageQueryRepository, ImageQueryRepository>();

        return services;
    }
}