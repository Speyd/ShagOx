using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Currencies;

namespace ShagOxServer.Infrastructure.DependencyInjections.Specification;
public static class CurrencyDependencyInjection
{
    public static IServiceCollection AddCurrencyInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<ICurrencyQueryRepository, CurrencyQueryRepository>();
        services.AddScoped<ICurrencyExistsRepository, CurrencyExistsRepository>();

        return services;
    }
}