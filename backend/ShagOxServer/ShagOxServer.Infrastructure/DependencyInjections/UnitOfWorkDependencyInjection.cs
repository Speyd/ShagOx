using Microsoft.Extensions.DependencyInjection;
using ShagOxServer.Application.Interfaces.Persistences;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Currencies;
using ShagOxServer.Infrastructure.Persistence;

namespace ShagOxServer.Infrastructure.DependencyInjections;
public static class UnitOfWorkDependencyInjection
{
    public static IServiceCollection AddUnitOfWorkInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}