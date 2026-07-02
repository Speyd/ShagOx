using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
using ShagOxServer.Infrastructure.Interfaces.Specification.Currencies;
using ShagOxServer.Infrastructure.Interfaces.Specification.Images;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Images;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class SpecificationDependencyInjection
{
    public static IServiceCollection AddSpecificationInfrastructure(this IServiceCollection services)
    {
        // Currency
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ICurrencyQueryRepository, CurrencyQueryRepository>();
        services.AddScoped<ICurrencyExistsRepository, CurrencyExistsRepository>();

        // Condition
        services.AddScoped<IConditionRepository, ConditionRepository>();
        services.AddScoped<IConditionQueryRepository, ConditionQueryRepository>();
        services.AddScoped<IConditionExistsRepository, ConditionExistsRepository>();

        // Image
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IImageQueryRepository, ImageQueryRepository>();
        services.AddScoped<IImageExistsRepository, ImageExistsRepository>();

        return services;
    }
}